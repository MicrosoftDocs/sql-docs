---
Title: "Tutorial: Using Templates in SQL Server Management Studio"
description: "A Tutorial for using Templates within SSMS. ." 
keywords: SQL Server, SSMS, SQL Server Management Studio, Templates
author: MashaMSFT
ms.author: mathoma
ms.date: 03/13/2018
ms.topic: Tutorial
ms.suite: "sql"
ms.prod_service: sql-tools
ms.reviewer: sstein
manager: craigg
helpviewer_keywords: 
  - "templates [SQL Server], SQL Server Management Studio"
  - "source controls [SQL Server Management Studio], tutorials"
  - "Help [SQL Server], SQL Server Management Studio"
  - "tutorials [SQL Server Management Studio]"
  - "Transact-SQL tutorials"
  - "SQL Server Management Studio [SQL Server], tutorials"
  - "scripts [SQL Server], SQL Server Management Studio"
---

# Tutorial: Using Templates within SQL Server Management Studio
This tutorial will introduce you to the pre-built Transact-SQL (T-SQL) templates that are available within SQL Server Management Studio (SSMS). In this article, you will learn how to:

> [!div class="checklist"]
> * Use the Template Browser to generate T-SQL Scripts
> * Edit an existing Template 
> * Locate the templates on disk
> * Create a new Template
   

## Prerequisites
To complete this Tutorial, you need SQL Server Management Studio, and access to a SQL Server. 

- Install [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).
- Install [SQL Server 2017 Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

 

## Using the Template Browser
In this section, you will learn how to locate and use the **Template Browser**. 

1. Start your SQL Server Management Studio.
2. From the **View** Menu > **Template Browser** (Ctrl + Alt + T): 

    ![Template Browser](media/templates-ssms/templatebrowser.png)
    - You can also see recently used templates at the bottom of the **Template Browser**.

3. Expand the node you're interested in > Right click the Template > Open:

    ![Open Template](media/templates-ssms/opentemplate.png)
    - Double-clicking the template will have the same effect.

4. This will launch a new query window with the T-SQL already populated. 
5. Modify the template to suit your needs and then **Execute** the query:
    
    ![Create DB Template](media/templates-ssms/createdbtemplate.png)


## Edit an Existing Template
You can also edit the existing templates within **Template Browser**.  

1. Locate the template of interest in the **Template Browser**.
2. Right-click the template > **Edit**:

    ![Edit Template](media/templates-ssms/edittemplate.png)

3. Make the desired changes in the query window that opens.
4. Save the template by going to **File** > **Save** (Ctrl + S).
5. Close the query window.
6. Reopen the Template you saved > your new edits should be there.
 

## Locate the Templates on Disk
Once a template is open, you can then locate it on disk.

1. Select a template in **Template Browser** > **Edit**.
2. Right-click the **Query Title** > **Open Containing Folder**. 
This should open your explorer to where the templates are stored on disk: 

    ![Templates On Disk](media/templates-ssms/templatesondisk.png)
  

## Create a New Template
Within the **Template Browser** you also have the ability to create new templates. These steps will teach you to create a new folder, and then create a new template within that folder. However, with these steps, you can also create a custom template within the existing folders. 

1. Open **Template Browser**.
2. Right-click SQL Server Templates > **New** > **Folder**.
3. Name this folder **Custom Templates**:

    ![Creating Custom Templates](media/templates-ssms/creatingcustomtemplate.png)

4. Right-click the newly created **Custom Templates** folder > **New** > **Template** > name your template:
 
    ![Create Custom Template](media/templates-ssms/createnewtemplate.png)
   
5. Right-click the template you just created > **Edit**. This will open a **New Query Window**.
6. Type in the T-SQL text you want to save. 
7. Save the file by going to the **File** menu > **Save**.
8. Close the existing **Query Window** and open your new custom template. 

    

## Next steps
The next article will provide some additional tips and tricks for using SQL Server Management Studio. 

Advance to the next article to learn more
> [!div class="nextstepaction"]
> [Next steps button](ssms-tricks.md)
