namespace UserExplorer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientsTableMigration : DbMigration
    {
        public override void Up()
        {
            //������ ������� ��������
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        //���� ID �������� ���������� ���������
                        ID = c.Int(nullable: false, identity: true),
                        Family = c.String(nullable: false),
                        Name = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            //�������� �������
            DropTable("dbo.Clients");
        }
    }
}
