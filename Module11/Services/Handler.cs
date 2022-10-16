namespace Module11.Services
{
    static class Handler
    {
        static public string Sum(string message)
        {
            string[] string_array = message.Split(' ');
            if (string_array.Length < 2)
            {
                return "Неверный формат сообщения";
            }
            int res = 0;
            foreach (string str in string_array)
            {
                int num;
                if (!int.TryParse(str, out num))
                {
                    return "Неверный формат сообщения";
                }
                res += num;
            }
            return String.Concat("Сумма равна: " + res.ToString());
        }
        static public string Count(string message)
        {
            return String.Concat("Длина сообщения равна: " + message.Length);
        }
    }
}
