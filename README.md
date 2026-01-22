# Porte Othoba Porate Chai 

C# Desktop Application.

## 🎥 Video Demonstration

<p align="left">
  <a href="https://mega.nz/file/090h3TRS#9_atoUxivKzHRZn24wZXJKDbzD4e0p2D1Zqf9yBAhHI" target="_blank">
    ▶️ <b>Click Here to Watch Application Demo Video</b>
  </a>
</p>


<h1 align="center">Hi 👋, I'm Md. Imtiaj Ahmed</h1>

## Table
MSSQL
```sql
CREATE TABLE Student (
    Id       INT    PRIMARY KEY       IDENTITY (1, 1) NOT NULL,
    Name     VARCHAR (50)  NOT NULL,
    PhoneNum VARCHAR (50)  NOT NULL,
    Email    VARCHAR (50)  NOT NULL,
    Password VARCHAR (50)  NOT NULL,
    Address  VARCHAR (100) NOT NULL,
    Gender   VARCHAR (10)  NOT NULL,
    Picture  VARCHAR (100) NULL,
    DOB      DATE          NOT NULL
);
````
```sql
CREATE TABLE Teacher (
    Id            INT   PRIMARY KEY       IDENTITY (100, 1) NOT NULL,
    Name          VARCHAR (50)  NOT NULL,
    PhoneNum      VARCHAR (50)  NOT NULL,
    Email         VARCHAR (50)  NOT NULL,
    Password      VARCHAR (50)  NOT NULL,
    Address       VARCHAR (100) NOT NULL,
    Gender        VARCHAR (10)  NOT NULL,
    Picture       VARCHAR (100) NULL,
    DOB           DATE          NOT NULL,
    Qualification VARCHAR (50)  NOT NULL
);
```
```sql
CREATE TABLE Info (
    Id             INT   PRIMARY KEY      IDENTITY (2000, 1) NOT NULL,
    SId            INT           NOT NULL,
    TId            INT           NOT NULL,
    TimeDuration   VARCHAR (20)  NOT NULL,
    StartingDate   DATE          NOT NULL,
    Salary         INT           NOT NULL,
    Description    VARCHAR (150) NULL,
    StudentStatus  VARCHAR (10)  NOT NULL,
    TeacherStatus  VARCHAR (10)  NOT NULL,
    AssignDate     DATE          NOT NULL,
    TeacherUpCheck INT           NOT NULL,
    StudentUpCheck INT           NOT NULL,
    CONSTRAINT FK1 FOREIGN KEY (SId) REFERENCES Student(Id),
    CONSTRAINT FK2 FOREIGN KEY (TId) REFERENCES Teacher(Id)
);
```
### Course Instructor
##### MD. MAZID-UL-HAQUE
<a href="https://www.linkedin.com/in/mdmazidulhaque/">
    <img src="https://www.vectorlogo.zone/logos/linkedin/linkedin-icon.svg" alt="Md. Mazid-Ul-Haque's LinkedIn Profile" height="30" width="30">
</a>

<h3 align="left">Connect with me:</h3>
<p align="left">
<a href="https://fb.com/imtiajahmedanik51" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/facebook.svg" alt="imtiajahmedanik57" height="30" width="40" /></a>
    <a href="https://www.linkedin.com/in/imtiajahmed51/" target="blank"><img align="center" src="https://www.vectorlogo.zone/logos/linkedin/linkedin-icon.svg" alt="Md. Imtiaj Ahmed's" height="30" width="40" /></a>
</p>


<h3 align="left">Languages and Tools:</h3>
<p align="left"> <a href="https://www.w3schools.com/cs/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40"/> </a> <a href="https://www.microsoft.com/en-us/sql-server" target="_blank" rel="noreferrer"> <img src="https://www.svgrepo.com/show/303229/microsoft-sql-server-logo.svg" alt="mssql" width="40" height="40"/> </a> </p>

<p><img align="center" src="https://github-readme-stats.vercel.app/api/top-langs?username=imtiajahmedanik&show_icons=true&locale=en&layout=compact" alt="imtiajahmedanik" /></p>
