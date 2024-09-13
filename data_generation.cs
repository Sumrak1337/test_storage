public enum PalletProperties
{
    length,
    width,
    height,
}

public enum BoxProperties
{
    length,
    width,
    height,
    production_date,
    expiration_date,
}

public class DataGeneration
{
    readonly int max_pallet_size = 100;
    readonly int max_box_size = 50;
    static Random random = new(42);

    public IEnumerable<Dictionary<string, double>> GeneratePalletProperties(int n_pallets) 
    {
        for (int i = 0; i < n_pallets; i++) {
            Dictionary<string, double> palletProperties = new()
            {
                ["ID"] = i
            };

            foreach (PalletProperties property in Enum.GetValues(typeof(PalletProperties)))
            {
                palletProperties[property.ToString()] = max_pallet_size * random.NextDouble();
            }
            yield return palletProperties;
        }
    }

    public IEnumerable<Dictionary<string, object>> GenerateBoxProperties(int n_boxes) 
    {
        for (int i = 0; i < n_boxes; i++) {
            Dictionary<string, object> boxProperties = new()
            {
                ["ID"] = i
            };

            foreach (BoxProperties property in Enum.GetValues(typeof(BoxProperties)))
            {   
                if (property.ToString().Contains("date")) {
                    
                    boxProperties[property.ToString()] = GetRandomDay();
                }
                else {
                    boxProperties[property.ToString()] = max_box_size * random.NextDouble();
                }
            }

            yield return boxProperties;
        }
    }

    static DateTime GetRandomDay() 
    {
        DateTime start = new(2000, 1, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(random.Next(range));
    }
}
