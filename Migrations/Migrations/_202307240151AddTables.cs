using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.Migrations
{
    [Migration(202307240151)]
    public class _202307240151AddTables :Migration
    {
        public override void Down()
        {
            Delete.Table("Unit");
            Delete.Table("Block");
            Delete.Table("Complex");
        }

        public override void Up()
        {
            // Start Tables
            Create.Table("Complex").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                   .WithColumn("Name").AsString().NotNullable()
                                   .WithColumn("NumberUnits").AsInt32().NotNullable();

            Create.Table("Block").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                  .WithColumn("Name").AsString().NotNullable()
                                  .WithColumn("NumberUnits").AsInt32().NotNullable()
                                  .WithColumn("ComplexId").AsInt32()
                                  .ForeignKey("FK_Blocks_Complexes", "Complex", "Id");

            Create.Table("Unit").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                .WithColumn("Tenant").AsString().NotNullable()
                                .WithColumn("BlockId").AsInt32();


            // End Tables


        }
    }
}
