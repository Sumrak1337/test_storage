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
    date,
}

public class DataGeneration
{
    static int box_idx = 0;
    readonly static int n_pallets = 1000;
    readonly int max_pallet_size = 100;
    readonly int min_pallet_size = 30;
    readonly int max_box_coefficient = 50;
    readonly static Random random = new(42);
    readonly static float threshold = 0.5f;
    readonly DateTime default_date = new DateTime(2024, 1, 1);

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
        int n_boxes = random.Next(1, 1000);
        int i = 0;
        while (i < n_boxes) {
            Dictionary<string, object> boxProperties = new()
            {
                ["ID"] = box_idx
            };

            foreach (BoxProperties property in Enum.GetValues(typeof(BoxProperties)))
            {   
                DateTime random_date = GetRandomDay(default_date);
                if (property.ToString().Contains("date")) {
                    // Generate production and expiration dates
                    bool production_condition = random.NextSingle() > threshold;
                    bool expiration_condition = random.NextSingle() > threshold;
                    if (production_condition || (!expiration_condition && !production_condition))
                    {
                        boxProperties["production_" + property.ToString()] = random_date;
                    }
                    if (expiration_condition)
                    {
                        boxProperties["expiration_" + property.ToString()] = GetRandomDay(new DateTime(random_date.Year, random_date.Month, random_date.Day));
                    }
                }
                else {
                    boxProperties[property.ToString()] = max_box_coefficient * random.NextSingle();
                }
            }
            i++;
            box_idx++;

            yield return boxProperties;
        }
    }

    static DateTime GetRandomDay(DateTime date)
    // Uses for random date generation
    {
        DateTime start = new(date.Year, date.Month, date.Day);
        int range = (new DateTime(2025, 1, 1) - start).Days;
        return start.AddDays(random.Next(range));
    }
}
