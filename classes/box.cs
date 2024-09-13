public class Box(Dictionary<string, object> dict)
{
    public int box_id = (int)dict["ID"];
    public float box_length = (float)dict["length"];
    public float box_width = (float)dict["width"];
    public float box_height = (float)dict["height"];
    public float box_weight = (float)dict["weight"];
    public DateTime production_date = (DateTime)dict["production_date"];
    public DateTime expiration_date = (DateTime)dict["expiration_date"];

    public float box_volume {
        get => box_height * box_width * box_length;
    }
}
