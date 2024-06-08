namespace PassionProject2024.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class component : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Components",
                c => new
                {
                    ComponentID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Type = c.String(),
                    Manufacturer = c.String(),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    ImagePath = c.String()
                })
                .PrimaryKey(t => t.ComponentID);
        }

        public override void Down()
        {
            DropTable("dbo.Components");
        }
    }
}
