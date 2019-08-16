using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EditorContentSaveInDB.Models
{
    public class EditorDataContext : DbContext
    {
        public EditorDataContext(DbContextOptions<EditorDataContext> options)
            : base(options)
        { }

        public DbSet<EditorData> EditorData { get; set; }
    }

    public class EditorData
    {

        [Key]
        public int ContentId { get; set; }
        public string EditorContent { get; set; }

    }
}
