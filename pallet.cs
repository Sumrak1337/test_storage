using System.Dynamic;

public class Pallet(Dictionary<string, float> dict, List<Box> boxes)
{
    // TODO: find a way, how to instantiate it
    public int ID = (int)dict["ID"];
    public float pallet_length = dict["length"];
    public float pallet_width = dict["width"];
    public float pallet_height = dict["height"];
    public List<Box> Boxes = boxes;
    public float pallet_weight
    {
        get 
        {   
            float weight = 30.0f;
            foreach (Box box in Boxes)
            {
                weight += box.box_weight;
            }
            return weight;
        }
    }

    public float pallet_volume 
    {
        get 
        {
            float volume = pallet_length * pallet_width * pallet_height;
            foreach (Box box in Boxes)
            {
                volume += box.box_volume;
            }
            return volume;
        }
    }    
}