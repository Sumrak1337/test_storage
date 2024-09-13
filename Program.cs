// Pallets and boxes generation
List<Pallet> pallets = [];

DataGeneration data_generation = new();
foreach (var pallet_dictionary in data_generation.GeneratePalletProperties())
{
    List<Box> boxes = [];
    foreach (var box_dictionary in data_generation.GenerateBoxProperties())
    {
        Box box = new(box_dictionary);
        if (box.box_length <= pallet_dictionary["length"] && box.box_width <= pallet_dictionary["width"])
        {
            boxes.Add(box);
        }
    }
    pallets.Add(new Pallet(pallet_dictionary, boxes));
}

// Grouping pallets
IEnumerable<IGrouping<DateTime, KeyValuePair<DateTime, IOrderedEnumerable<Pallet>>>> grouped_pallets = pallets
    .GroupBy(pallet => pallet.pallet_expiration_date)
    .OrderBy(pallet => pallet.Key)
    .ToDictionary(pallet => pallet.Key, pallet => pallet.OrderBy(pallet => pallet.pallet_weight))
    .GroupBy(pallet => pallet.Key);

Console.WriteLine();

foreach (IGrouping<DateTime, KeyValuePair<DateTime, IOrderedEnumerable<Pallet>>> grouped_pallet in grouped_pallets)
    {
        Console.WriteLine($"Expiration Date: {grouped_pallet.Key}");
        foreach (KeyValuePair<DateTime, IOrderedEnumerable<Pallet>> pallet_dict in grouped_pallet)
        {
            foreach (Pallet pallet in pallet_dict.Value) {
                Console.WriteLine($"ID: {pallet.ID} | weight: {pallet.pallet_weight}");
            }
        }
        Console.WriteLine("");
    }
Console.WriteLine();

// Increase in volume
Console.WriteLine("Increase in volume");
IOrderedEnumerable<Pallet> volume_pallets = pallets
.OrderBy(pallet => pallet.pallet_expiration_date)
.Reverse()
.SkipLast(pallets.Count - 3)
.OrderBy(pallet => pallet.pallet_weight);

foreach (Pallet pallet in volume_pallets)
{
    Console.WriteLine($"Expiration Date: {pallet.pallet_expiration_date}");
    Console.WriteLine($"ID: {pallet.ID} | volume: {pallet.pallet_volume}");
}
