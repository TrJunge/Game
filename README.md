# Game

# Server
- Лобби
{
    Получение:
    Отправка:
}
- Авторизация 
{
    Получение:
    Отправка:
}
- Регистрация
{
    Получение:
    Отправка:
}
# Client
- Лобби
{
    Отправка:
        Создание лобби - nameLobby + type + countPlayers + createlobby;
        Вход в лобби - nameLobby + joinLobby;
        Выход из лобби - nameLobby + exitLobby.
    Получение:
}
- Авторизация
{
    Отправка:
        Авторизация - login + password + nickname + auth.
    Получение:
}
- Регистрация
{
    Отправка:
        Регистрация - login + password + reg.
    Получение:
}
- Главная меню
{
    Отправка:
        Смена ника - nickname + changeNickname.
    Получение:
}