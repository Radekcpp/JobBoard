namespace JobBoard.web
{
    public static class SD
    {
        public static string JobsAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE,
        }
    }
}
