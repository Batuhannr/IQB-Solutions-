﻿namespace IQBInterview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMig : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ExamResults", new[] { "StudentId" });
            DropIndex("dbo.ExamResults", new[] { "CourseId" });
            CreateIndex("dbo.ExamResults", new[] { "StudentId", "CourseId" }, name: "Exam");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ExamResults", "Exam");
            CreateIndex("dbo.ExamResults", "CourseId");
            CreateIndex("dbo.ExamResults", "StudentId");
        }
    }
}
