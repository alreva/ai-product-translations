using System.Text.Json;

namespace AiProductTranslations.ApiService.Catalog;

partial class RedisCacheCatalogService : ICatalogService
{
    public Task<CatalogProduct[]> SampleCatalog()
    {
        return Task.FromResult(JsonSerializer.Deserialize<CatalogProduct[]>(
            """
            [
                {
                    "productName": "Men's Running Shoes",
                    "features": ["Lightweight", "Breathable fabric", "Cushioned insole", "Durable rubber outsole"],
                    "color": "Blue",
                    "sizeRange": "7-12",
                    "price": "$80.00"
                },
                {
                    "productName": "Wireless Bluetooth Headphones",
                    "features": ["Noise cancellation", "20-hour battery life", "Comfort-fit ear cups", "Built-in microphone"],
                    "color": "Black",
                    "sizeRange": "One size",
                    "price": "$120"
                },
                {
                    "productName": "Portable Power Bank",
                    "features": ["10000mAh capacity", "Dual USB ports", "LED power indicator", "Fast charging"],
                    "color": "Silver",
                    "sizeRange": "N/A",
                    "price": "$35"
                },
                {
                    "productName": "Eco-Friendly Water Bottle",
                    "features": ["BPA-free material", "Insulated design", "Leak-proof cap", "24 oz capacity"],
                    "color": "Green",
                    "sizeRange": "N/A",
                    "price": "$25",
                    "description": "Introducing our Eco-Friendly Water Bottle - the perfect solution for staying hydrated on-the-go while also being environmentally conscious. Made from BPA-free material, this water bottle is safe for both you and the planet. The insulated design keeps your drinks at the perfect temperature for hours, whether it's ice-cold water on a hot day or piping hot coffee on a chilly morning. The leak-proof cap ensures that you can toss it in your bag without worrying about spills. With a generous 24 oz capacity, you can easily stay hydrated throughout the day. Available in a vibrant green color, this water bottle is not only functional but also stylish. Say goodbye to single-use plastic bottles and join the eco-friendly movement with our durable and reusable water bottle. Priced at $25, it is a small investment towards a healthier planet and a healthier you. Get your Eco-Friendly Water Bottle today and make a positive impact on the environment."
                },
                {
                    "productName": "Smart Fitness Watch",
                    "features": ["Heart rate monitor", "Sleep tracking", "Waterproof design", "GPS integration"],
                    "color": "Blue",
                    "sizeRange": "One size",
                    "price": "$199"
                },
                {
                    "productName": "Compact Camping Tent",
                    "features": ["2-person capacity", "Lightweight design", "Waterproof material", "Easy setup"],
                    "color": "Red",
                    "sizeRange": "N/A",
                    "price": "$150"
                },
                {
                    "productName": "Men's Polarized Sunglasses",
                    "features": ["UV protection", "Scratch-resistant lenses", "Durable frame", "Classic style"],
                    "color": "Brown",
                    "sizeRange": "One size",
                    "price": "$80"
                },
                {
                    "productName": "Women's Yoga Mat",
                    "features": ["Non-slip surface", "Eco-friendly material", "Extra-thick padding", "Carry strap included"],
                    "color": "Purple",
                    "sizeRange": "N/A",
                    "price": "$45"
                },
                {
                    "productName": "Electric Toothbrush",
                    "features": ["Rechargeable battery", "Multiple brushing modes", "Pressure sensor", "Timer function"],
                    "color": "White",
                    "sizeRange": "N/A",
                    "price": "$90"
                },
                {
                    "productName": "Travel Backpack",
                    "features": ["30-liter capacity", "Laptop compartment", "Water-resistant", "Adjustable straps"],
                    "color": "Grey",
                    "sizeRange": "N/A",
                    "price": "$70"
                },
                {
                    "productName": "Digital Kitchen Scale",
                    "features": ["Precision sensors", "LCD display", "Tare function", "Stainless steel platform"],
                    "color": "Black",
                    "sizeRange": "N/A",
                    "price": "$30"
                }
            ]
            """
            , JsonOptions.Default))!;
    }
}