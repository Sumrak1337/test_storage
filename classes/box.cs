public class Box(Dictionary<string, object> dict)
{
    public int box_id = (int)dict["ID"];
    public float box_length = (float)dict["length"];
    public float box_width = (float)dict["width"];
    public float box_height = (float)dict["height"];
    public float box_weight = (float)dict["weight"];
    public DateTime expiration_date
    {
        get
        {
            if (dict.TryGetValue("expiration_date", out object? value))
            {
                return (DateTime)value;
            }
            else
            {
                return ((DateTime)dict["production_date"]).AddDays(100);
            }
        }
    }

    public float box_volume {
        get => box_height * box_width * box_length;
    }
}
