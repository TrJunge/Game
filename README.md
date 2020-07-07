# Server
- Лобби
    #### Получение:
        Создание лобби - nameLobby + type + countPlayers + createlobby
        Вход в лобби - nameLobby + joinLobby
        Выход из лобби - nameLobby + exitLobby
    #### Отправка:
- Авторизация 
    #### Получение:
    #### Отправка:
- Регистрация
    #### Получение:
    #### Отправка:
# Client
- Лобби
    ## Отправка:
        Создание лобби - nameLobby + type + countPlayers + createlobby
        Вход в лобби - nameLobby + joinLobby
        Выход из лобби - nameLobby + exitLobby
    ## Получение:
- Авторизация
    ## Отправка:
        Авторизация - login + password + nickname + auth
    ## Получение:
         В случае если авторизация прошла:
            Успешно: Okauth
            Провалено: Failauth
- Регистрация
    ## Отправка:
        Регистрация - login + password + reg
    ## Получение: 
        В случае если регистрация прошла:
            Успешно:Okreg
            Провалена:Failreg
- Главная меню
    ## Отправка:
        Смена ника - nickname + changeNickname
    ## Получение:
        Смена ника - OkchangeNickname