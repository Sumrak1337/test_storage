// Pallets and boxes generation
List<Pallet> pallets = [];

DataGeneration data_generation = new();
foreach (var pallet_dictionary in data_generation.GeneratePalletProperties())
{
    List<Box> boxes = [];
    foreach (var box_dictionary in data_generation.GenerateBoxProperties())
    {
        boxes.Add(new Box(box_dictionary));
    }
    // TODO: add the condition for boxes
    pallets.Add(new Pallet(pallet_dictionary, boxes));
}
