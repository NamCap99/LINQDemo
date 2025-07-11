﻿using LINQDemo;

namespace LINQDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var brands = new List<Brand>() {
                new Brand{ID = 1, Name = "Công ty AAA"},
                new Brand{ID = 2, Name = "Công ty BBB"},
                new Brand{ID = 4, Name = "Công ty CCC"},};

            var products = new List<Product>(){
                new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
                new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
                new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
                new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
                new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
                new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
                new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),};

            // Get product = 400
            var query = from p in products
                        where p.Price == 400
                        select p;

            System.Console.WriteLine("Get product = 400");
            foreach (var product in query)
            {
                System.Console.WriteLine(product);
            }
            System.Console.WriteLine("\n--------------------------------");
            // Select
            System.Console.WriteLine("Select in LINQ");
            // var result = products.Select(p => p.Name);
            var result = products.Select(
                p => (
                    p.Price,
                    p.Name,
                    p.Colors
                )
            );

            foreach (var productName in result)
            {
                // Join all colors with a comma (or any separator you like)
                string colorsJoined = string.Join(", ", productName.Colors);
                Console.WriteLine($"{productName.Name} – {productName.Price} – {colorsJoined}");
            }
            System.Console.WriteLine("\n--------------------------------");
            // Where 
            System.Console.WriteLine("Where in LINQ");
            var result2 = products.Where(
                // p => p.Price > 300,
                products => products.Brand == 2
                // OrderBy --> Acsending
                // OrderByDescending --> Descending
                ).OrderByDescending(
                    p => p.Price
                );

            foreach (var product in result2)
            {
                System.Console.WriteLine(product);
            }
            System.Console.WriteLine("\n--------------------------------");
            System.Console.WriteLine("Select many in LINQ");
            // SelectMany
            var result3 = products.SelectMany(
                p => p.Colors,
                (p, color) => new { p.Name, color }
            );
            foreach (var item in result3)
            {
                Console.WriteLine($"{item.Name} - {item.color}");
            }

            //Min, Max, Sum, Average
            System.Console.WriteLine("\n--------------------------------");
            System.Console.WriteLine("Min, Max, Sum, Average in LINQ");
            var minPrice = products.Min(products => products.Price);
            var maxPrice = products.Max(products => products.Price);
            var sumPrice = products.Sum(products => products.Price);
            var averagePrice = products.Average(products => products.Price);
            Console.WriteLine($"Min Price: {minPrice}");
            Console.WriteLine($"Max Price: {maxPrice}");
            Console.WriteLine($"Sum Price: {sumPrice}");
            Console.WriteLine($"Average Price: {averagePrice}");

            // GroupBy
            System.Console.WriteLine("\n--------------------------------");
            System.Console.WriteLine("GroupBy in LINQ");
            var groupedProducts = products.GroupBy(
                p => p.Brand,
                (brand, productGroup) => new
                {
                    Brand = brand,
                    Products = productGroup.ToList()
                }
            );

            foreach (var group in groupedProducts)
            {
                Console.WriteLine($"Brand: {group.Brand}");
                foreach (var product in group.Products)
                {
                    Console.WriteLine($"  Product: {product.Name} - Price: {product.Price}");
                }
            }
            System.Console.WriteLine("\n--------------------------------");
            // Join
            System.Console.WriteLine("Join in LINQ");
            var joinedProducts = brands.Join(products, b => b.ID, p => p.Brand, (p, b)
                =>
            {
                return new
                {
                    productName = p.Name,
                    brandName = b.Name
                };
            });
            foreach (var item in joinedProducts)
            {
                System.Console.WriteLine(item);
            }

            // var joinedProductsCount = joinedProducts.ToList();

            // for (int item = 0; item < joinedProductsCount.Count; item++)
            // {
            //     System.Console.WriteLine(joinedProductsCount[item]);
            // }
            System.Console.WriteLine("\n--------------------------------");
            // GroupJoin
            System.Console.WriteLine("GroupJoin in LINQ");
            var groupJoined = brands.GroupJoin(products, b => b.ID, p => p.Brand,
            (bra, pro) =>
            {
                return new
                {
                    productName = bra.Name,
                    AllBrand = pro
                };
            });

            foreach (var gj in groupJoined)
            {
                System.Console.WriteLine(gj.productName);
                foreach (var p in gj.AllBrand)
                {
                    System.Console.WriteLine(p);
                }
            }
            System.Console.WriteLine("\n--------------------------------");
            // Take
            System.Console.WriteLine("Take method in LINQ");
            // var takeInLINQ = products.Take(3).ToList();

            // foreach (var t in takeInLINQ)
            // {
            //     System.Console.WriteLine(t);
            // }

            // Or we can code shoter like this
            products.Take(3).ToList().ForEach(p => System.Console.WriteLine(p));
            System.Console.WriteLine("\n--------------------------------");
            // Skip
            System.Console.WriteLine("Skip in LINQ");
            products.Skip(2).ToList().ForEach(p => System.Console.WriteLine(p));
            System.Console.WriteLine("\n--------------------------------");
            // Reverse
            System.Console.WriteLine("Reserve: ");
            int[] numbers = { 1, 2, 3, 2, 5, 9, 10, 4, 5, 6 };
            numbers.Reverse().ToList().ForEach(n => System.Console.WriteLine(n));

            System.Console.WriteLine("\n--------------------------------");

            // Distinct in LINQ
            System.Console.WriteLine("Return the list with no duplicate value");
            System.Console.WriteLine("Distinct: ");
            products.SelectMany(p => p.Colors).Distinct().ToList().ForEach(c => System.Console.WriteLine(c));

            // Single or SingleOrDefault
            System.Console.WriteLine("\n--------------------------------");
            System.Console.WriteLine("Single method in LINQ\nReturn when found a value and return error when not found");
            var pInSingle = products.SingleOrDefault(p => p.Price == 1300);
            System.Console.WriteLine(pInSingle);
            System.Console.WriteLine("\n--------------------------------");
            // Any() method
            System.Console.WriteLine("Any in LINQ\nReturn true when found and return false when not found the condition");
            var anyInLinq = products.Any(p => p.Price == 300);
            System.Console.WriteLine($"Return {anyInLinq} when found");

            var anyInLinq2 = products.Any(p => p.Price == 1200);
            System.Console.WriteLine($"Return {anyInLinq2} when not found");

            System.Console.WriteLine("\n--------------------------------");
            // Count() method
            System.Console.WriteLine("Count() method in LINQ");
            var countInLINQ = products.Count(p => p.Price >= 300);
            System.Console.WriteLine($"Total = {countInLINQ}");

        }
    }
}

