using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.Migrations
{
    [Migration(202307272257)]
    public class _202307272257AddTables :Migration
    {
        public override void Down()
        {
            Delete.Table("Unit");
            Delete.Table("Block");
            Delete.Table("Complex");
        }

        public override void Up()
        {
            Create.Column("TypeHouse").OnTable("Unit").AsString().Nullable();
        }
    }
}
