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
