using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Expensify.Entities.Interfaces;

namespace Expensify.DataAccessLayer.Entities.AbstractClasses;

public abstract class Identifiable : IIdentifiable
{
    [Key]
    [Column(Order = 1)]
    public Guid Id { get; set; }

    [Column("deletion_indicator")]
    public bool IsDeleted { get; set; }
    public Guid LastUpdatedBy { get; set; }
    public Guid CreatedBy { get; set; }
    public long CreateDate { get; set; }
    public long UpdatedDate { get; set; }

    protected Identifiable()
    {
        Id = Guid.NewGuid();
        TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1);
        int secSinceEpoc = (int)timeSpan.TotalSeconds;
        CreateDate = secSinceEpoc;
        UpdatedDate = secSinceEpoc;
    }
}
