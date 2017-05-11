namespace UserExplorer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientsTableMigration : DbMigration
    {
        public override void Up()
        {
            //Содаем таблицу клиентов
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        //Поле ID является уникальным значением
                        ID = c.Int(nullable: false, identity: true),
                        Family = c.String(nullable: false),
                        Name = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            //Удаление таблицы
            DropTable("dbo.Clients");
        }
    }
}
