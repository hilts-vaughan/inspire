namespace GameServer
{
    /// <summary>
    /// A static global class
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// This is the private key shared between application server clusters.
        /// Application servers unable to provide this key are rejected.
        /// </summary>
        public static string PrivateKey = "6673abf8184ab669a60f7f63b9326de495f3c268733f6ff4cf749f3a15d450fc";

    }
}
