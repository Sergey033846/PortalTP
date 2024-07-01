var CADESCOM_CADES_BES = 1;
//var CADESCOM_CADES_T = 5;
var CAPICOM_CURRENT_USER_STORE = 2;
    var CAPICOM_MY_STORE = "My";
    var CAPICOM_STORE_OPEN_MAXIMUM_ALLOWED = 2;
    var CAPICOM_CERTIFICATE_FIND_SUBJECT_NAME = 1;
    var CADESCOM_HASH_ALGORITHM_CP_GOST_3411 = 100;
var CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_256 = 101
var CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_512 = 102

function CreateObject(name) 
{
        switch (navigator.appName) {

            case 'Microsoft Internet Explorer':
            return new ActiveXObject(name);

            default:

            var userAgent = navigator.userAgent;

            if (userAgent.match(/Trident\/./i)) { // IE10, 11

                return new ActiveXObject(name);
        }

        var cadesobject = document.getElementById('cadesplugin');

        return cadesobject.CreateObject(name);

        }
}

function selectCertificate() 
{
        var CAPICOM_STORE_OPEN_READ_ONLY = 0;
        var CAPICOM_CURRENT_USER_STORE = 2;

        try {
            var CertStore = CreateObject("CAPICOM.Store");
        } catch (err) {
            alert('Failed to create CAPICOM.store: ' + err.number);
            return;
        }

        //Открываем хранилище сертификатов пользователя только для чтения
            CertStore.Open(CAPICOM_CURRENT_USER_STORE, "MY", CAPICOM_STORE_OPEN_READ_ONLY);

            //Выводим пользователю окно выбора сертификата
        try {
            var certificate = CertStore.Certificates.Select(
            "Выберите сертификат для подписи документа."
            , "Выберите один из сертификатов", false);
        }
        catch (e)
        {
            alert('ошибка сертификат'+ e.message);
            // Пользователь не выбрал сертификат
        }
        return certificate.Item(1);
}


    function GetCertificateBySubjectName(certSubjectName) {
        var oStore = cadesplugin.CreateObject("CAdESCOM.Store");
        oStore.Open(CAPICOM_CURRENT_USER_STORE, CAPICOM_MY_STORE,
            CAPICOM_STORE_OPEN_MAXIMUM_ALLOWED);

        var oCertificates = oStore.Certificates.Find(
            CAPICOM_CERTIFICATE_FIND_SUBJECT_NAME, certSubjectName);
        if (oCertificates.Count == 0) {
            alert("Certificate not found: " + certSubjectName);
            return;
        }
        var oCertificate = oCertificates.Item(1);

        return oCertificate;
    }

    function InitializeHashedData(hashAlg, sHashValue) {
        // Создаем объект CAdESCOM.HashedData
        var oHashedData = cadesplugin.CreateObject("CAdESCOM.HashedData");

        // Инициализируем объект заранее вычисленным хэш-значением
        // Алгоритм хэширования нужно указать до того, как будет передано хэш-значение
        oHashedData.Algorithm = hashAlg;
        oHashedData.SetHashValue(sHashValue);

        return oHashedData;
    }

    function CreateSignature(oCertificate, oHashedData) {

        // Создаем объект CAdESCOM.CPSigner
        var oSigner = cadesplugin.CreateObject("CAdESCOM.CPSigner");
	oSigner.Options = 1; //CAPICOM_CERTIFICATE_INCLUDE_WHOLE_CHAIN
        oSigner.Certificate = oCertificate;
	//oSigner.TSAAddress = "http://qs.cryptopro.ru/tsp/tsp.srf";

        // Создаем объект CAdESCOM.CadesSignedData
        var oSignedData = cadesplugin.CreateObject("CAdESCOM.CadesSignedData");

        var sSignedMessage = "";

        // Вычисляем значение подписи
        try {
            sSignedMessage = oSignedData.SignHash(oHashedData, oSigner, CADESCOM_CADES_BES);
//            sSignedMessage = oSignedData.SignHash(oHashedData, oSigner, CADESCOM_CADES_T);
        } catch (err) {
            alert("Failed to create signature. Error: " + cadesplugin.getLastError(err));
            return;
        }

        return sSignedMessage;
    }

    function CreateSignature2(oCertificate, oHashedData) {

        // Создаем объект CAdESCOM.RawSignature
        var oRawSignature = cadesplugin.CreateObject("CAdESCOM.RawSignature");

        // Вычисляем значение подписи
        try {
            var sRawSignature = oRawSignature.SignHash(oHashedData, oCertificate);
        } catch (err) {
            alert("Failed to create signature. Error: " + cadesplugin.getLastError(err));
            return;
        }

        return sRawSignature;
    }

    function VerifySignature(oHashedData, sSignedMessage) {
        // Создаем объект CAdESCOM.CadesSignedData
        var oSignedData = cadesplugin.CreateObject("CAdESCOM.CadesSignedData");

        // Проверяем подпись
        try {
            oSignedData.VerifyHash(oHashedData, sSignedMessage, CADESCOM_CADES_BES);
        } catch (err) {
            alert("Failed to verify signature. Error: " + cadesplugin.getLastError(err));
            return false;
        }

        return true;
    }

    function run() {
//        var oCertName = document.getElementById("CertName");
//        var sCertName = oCertName.value; // Здесь следует заполнить SubjectName сертификата
//        if ("" == sCertName) {
//            alert("Введите имя сертификата (CN).");
//            return;
//        }
        // Ищем сертификат для подписи
	// var oCertificate = GetCertificateBySubjectName(sCertName);
	var oCertificate = selectCertificate();

//	var signMethod = "";
//	var digestMethod = "";

	var pubKey = oCertificate.PublicKey();
	var algo = pubKey.Algorithm;
	var algoOid = algo.Value;

        // Предварительно вычисленное хэш-значение исходных данных
        // Хэш-значение должно быть представлено в виде строки шестнадцатеричных цифр,
        // группами по 2 цифры на байт, с пробелами или без пробелов.
        // Например, хэш-значение в таком формате возвращают объекты
        // CAPICOM.HashedData и CADESCOM.HashedData.

    //var sHashValue = "F5D0E06E43A5F82E07D50C936CD4C8D7DE31554105F1FE5947A2018AF68FF7CB"; // убрал 23.07.2018
    var sHashValue_Gost3411 = ASPxHiddenFieldESign2.Get('ESignHash_Gost3411');
    var sHashValue_Gost3411_2012_256 = ASPxHiddenFieldESign2.Get('ESignHash_Gost3411_2012_256');
    var sHashValue_Gost3411_2012_512 = ASPxHiddenFieldESign2.Get('ESignHash_Gost3411_2012_512');

        // Алгоритм хэширования, при помощи которого было вычислено хэш-значение
        // Полный список поддерживаемых алгоритмов указан в перечислении CADESCOM_HASH_ALGORITHM

	var hashAlg = "";
	var sHashValue = "";
    if (algoOid == "1.2.643.7.1.1.1.1") {   // Р°Р»РіРѕСЂРёС‚Рј РїРѕРґРїРёСЃРё Р“РћРЎРў Р  34.10-2012 СЃ РєР»СЋС‡РѕРј 256 Р±РёС‚
//        signMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256";
//        digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256";
	hashAlg = CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_256; // ГОСТ 
	sHashValue = sHashValue_Gost3411_2012_256;
    }
    else if (algoOid == "1.2.643.7.1.1.1.2") {   // Р°Р»РіРѕСЂРёС‚Рј РїРѕРґРїРёСЃРё Р“РћРЎРў Р  34.10-2012 СЃ РєР»СЋС‡РѕРј 512 Р±РёС‚
//        signMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-512";
//        digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-512";
	hashAlg = CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_512; // ГОСТ 
	sHashValue = sHashValue_Gost3411_2012_512;
    }
    else if (algoOid == "1.2.643.2.2.19") {  // Р°Р»РіРѕСЂРёС‚Рј Р“РћРЎРў Р  34.10-2001
//        signMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102001-gostr3411";
//        digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr3411";
        hashAlg = CADESCOM_HASH_ALGORITHM_CP_GOST_3411;
	sHashValue = sHashValue_Gost3411;
    }
//        var hashAlg = CADESCOM_HASH_ALGORITHM_CP_GOST_3411; // ГОСТ Р 34.11-94
//        var hashAlg = CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_256; // ГОСТ 

        // Создаем объект CAdESCOM.HashedData
        var oHashedData = InitializeHashedData(hashAlg, sHashValue);

        /*document.getElementById('hash1').innerHTML = sHashValue_Gost3411;
        document.getElementById('hash2').innerHTML = sHashValue_Gost3411_2012_256;
        document.getElementById('hash3').innerHTML = sHashValue_Gost3411_2012_512;
        document.getElementById('hash0').innerHTML = sHashValue;*/

        // Создаем подписанное сообщение
        // Такая подпись должна проверяться в КриптоАРМ и cryptcp.exe
        var sSignedMessage = CreateSignature(oCertificate, oHashedData);
//        var sSignedMessage = CreateSignature2(oCertificate, oHashedData);

        //document.getElementById('signatureclient').innerHTML = algoOid;        
        //document.getElementById("<%=ASPxMemo1.ClientID %>").innerHTML = sSignedMessage;
        
        ASPxHiddenFieldESign3.Set('ESign', sSignedMessage);
        //document.getElementById('signatureclient').innerHTML = ASPxHiddenFieldESign3.Get('ESign');

        //ASPxHiddenFieldESign3.Set("ESign", "test");
                
//        var verifyResult = VerifySignature(oHashedData, sSignedMessage);
  //      if (verifyResult) {
    //        alert("Signature verified");
      //  }
	//else alert("Signature not verified");
    }
