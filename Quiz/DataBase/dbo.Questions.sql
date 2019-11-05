CREATE TABLE [dbo].[Questions] (
    [Id]             INT        NOT NULL,
    [QuestionNumber] INT        NOT NULL,
    [QuestionText]   NCHAR (10) NOT NULL,
    [TimeToAnswer]   TIME (7)   NOT NULL,
    [Points]         INT        NOT NULL,
    [ImagePath]      NCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);