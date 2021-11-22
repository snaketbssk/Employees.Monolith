namespace Employees.Monolith.Api.Authentications.Models.Constants
{
    public class GroupConstant
    {
        /// <summary>
        /// Все пользователи администраторы
        /// </summary>
        public const string ADMINISTRATORS = "Service,Admin";
        /// <summary>
        /// Сервис пользователь
        /// </summary>
        public const string SERVICE = "Service";
        /// <summary>
        /// Админ пользователь
        /// </summary>
        public const string ADMIN = "Admin";
        /// <summary>
        /// Обычный пользователь
        /// </summary>
        public const string USER = "User";
        /// <summary>
        /// Бот пользователь
        /// </summary>
        public const string BOT = "Bot";
        /// <summary>
        /// Все пользователи
        /// </summary>
        public const string IDENTITY = "Service,Admin,User,Bot";
        /// <summary>
        /// Все люди
        /// </summary>
        public const string HUMAN = "Admin,User";
    }
}
