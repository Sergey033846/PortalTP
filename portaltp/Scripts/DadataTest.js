$("#address").suggestions({
        token: "9bfcf5339af57a6b7a2959bbbaea81712ffaa79e",
        type: "ADDRESS",
        /* Вызывается, когда пользователь выбирает одну из подсказок */
        onSelect: function(suggestion) {
            console.log(suggestion);
        }
});