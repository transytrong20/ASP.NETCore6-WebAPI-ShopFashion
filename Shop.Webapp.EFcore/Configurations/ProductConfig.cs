using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Seedings;
using Shop.Webapp.Shared.ConstsDatas;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Webapp.EFcore.Configurations
{
    public static class ProductConfig
    {
        public static void CategoryProductConfiguration([NotNull] this ModelBuilder builder)
        {
            builder.Entity<Category>()
                 .ToTable($"{ApplicationConsts.Schema}_{nameof(Category).ToLower()}")
                 .HasQueryFilter(x => !x.IsDeleted);

            builder.Entity<Product>()
                .ToTable($"{ApplicationConsts.Schema}_{nameof(Product).ToLower()}")
                .HasQueryFilter(x => !x.IsDeleted);

            builder.Entity<CategoryProduct>()
                .ToTable($"{ApplicationConsts.Schema}_category_product")
                .HasKey(_ => new { _.CategoryId, _.ProductId });

            builder.Entity<Cart>()
                .ToTable($"{ApplicationConsts.Schema}_cart")
                .HasKey(_ => new { _.UserId, _.ProductId });
        }
    }
}
