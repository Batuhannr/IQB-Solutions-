namespace IQBInterview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMig1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ExamResults", "Exam");
            CreateIndex("dbo.ExamResults", new[] { "StudentId", "CourseId" }, unique: true, name: "Exam");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ExamResults", "Exam");
            CreateIndex("dbo.ExamResults", new[] { "StudentId", "CourseId" }, name: "Exam");
        }
    }
}
