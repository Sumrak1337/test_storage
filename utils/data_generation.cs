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
    weight,
    production_date,
    expiration_date,
}

public class DataGeneration
{
    int box_idx = 0;
    readonly static int n_pallets = 10;
    readonly int max_pallet_size = 100;
    readonly int min_pallet_size = 30;
    readonly int max_box_coefficient = 50;
    readonly static Random random = new(42);

    public IEnumerable<Dictionary<string, float>> GeneratePalletProperties()
    // Creates properties (id, length, width, height) for each pallet
    {
        for (int i = 0; i < n_pallets; i++) {
            Dictionary<string, float> palletProperties = new()
            {
                ["ID"] = i
            };

            foreach (PalletProperties property in Enum.GetValues(typeof(PalletProperties)))
            {
                palletProperties[property.ToString()] = (max_pallet_size - min_pallet_size) * random.NextSingle() + min_pallet_size;
            }
            yield return palletProperties;
        }
    }

    public IEnumerable<Dictionary<string, object>> GenerateBoxProperties()
    // Creates properties (id, length, width, height, weight, production and expiration dates) for each box
    {
        int n_boxes = random.Next(1, 10);
        for (int i = 0; i < n_boxes; i++) {
            Dictionary<string, object> boxProperties = new()
            {
                ["ID"] = box_idx
            };

            foreach (BoxProperties property in Enum.GetValues(typeof(BoxProperties)))
            {   
                if (property.ToString().Contains("date")) {
                    // TODO: add a random property generation
                    boxProperties[property.ToString()] = GetRandomDay().Date;
                }
                else {
                    boxProperties[property.ToString()] = max_box_coefficient * random.NextSingle();
                }
            }
            box_idx += 1;

            yield return boxProperties;
        }
    }

    static DateTime GetRandomDay()
    // Uses for random date generation
    {
        DateTime start = new(2000, 1, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(random.Next(range));
    }
}
