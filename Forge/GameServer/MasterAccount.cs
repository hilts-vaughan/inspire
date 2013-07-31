namespace GameServer
{
    /// <summary>
    /// A <see cref="MasterAccount"/> is a user object representing the master account of a given user.
    /// This object contains credentials and other basic utilties required by services.
    /// </summary>
    public class MasterAccount
    {
        /// <summary>
        /// The username of the master account associated
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password of the master account in question
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The e-mail associated with this master account
        /// </summary>
        public string Email { get; set; }

    }
}
