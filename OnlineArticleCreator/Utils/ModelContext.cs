using Microsoft.EntityFrameworkCore;

namespace CGT.AspNetMvc
{
    // Based off Christian Nagel's example context class in his tutorial for 
    // Unit Testing in EF Core 1.0
    // https://csharp.christiannagel.com/2016/11/22/efcoreunittesting/

    /// <summary>
    /// To help with unit-testing model classes interacting with databases.
    /// </summary>
    [System.Obsolete("Does not play nice with unit testing.")]
    public class ModelContext<TModel> : DbContext where TModel: class
    {
        public ModelContext() : base() { } // Obligatory

        public ModelContext(DbContextOptions<ModelContext<TModel>> options)
        : base(options) { }
        public DbSet<TModel> Models { get; set; }
    }
}
