---
title: "Use a Recordset Destination | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Recordset destination"
ms.assetid: a7b143dc-8008-404f-83b0-b45ffbca6029
author: janinezhang
ms.author: janinez
manager: craigg
---
# Use a Recordset Destination
  The Recordset destination does not save data to an external data source. Instead, the Recordset destination saves data in memory in a recordset that is stored in an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package variable of the `Object` data type. After the Recordset destination saves the data, you typically use a Foreach Loop container with the Foreach ADO enumerator to process one row of the recordset at a time. The Foreach ADO enumerator saves the value from each column of the current row into a separate package variable. Then, the tasks that you configure inside the Foreach Loop container read those values from the variables and perform some action with them.  
  
 You can use the Recordset destination in many different scenarios. Here are some examples:  
  
-   You can use a Send Mail task and the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] expression language to send a customized e-mail message for each row in the recordset.  
  
-   You can use a Script component configured as a source, inside a Data Flow task, to read the column values into the columns of the data flow. Then, you can use transformations and destinations to transform and save the row. In this example, the Data Flow task runs once for each row.  
  
 The following sections first describe the general process of using the Recordset destination and then show a specific example of how to use the destination.  
  
## General Steps to Using a Recordset Destination  
 The following procedure summarizes the steps that are required to save data to a Recordset destination, and then use the Foreach Loop container to process each row.  
  
#### To save data to a Recordset destination and process each row by using the Foreach Loop container  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], create or open an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package.  
  
2.  Create a variable that will contain the recordset saved into memory by the Recordset destination, and set the variable's type to `Object`.  
  
3.  Create additional variables of the appropriate types to contain the values of each column in the recordset that you want to use.  
  
4.  Add and configure the connection manager required by the data source that you plan to use in your data flow.  
  
5.  Add a Data Flow task to the package, and on the Data Flow tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, configure sources and transformations to load and transform the data.  
  
6.  Add a Recordset destination to the data flow and connect it to the transformations. For the `VariableName` property of the Recordset destination, enter the name of the variable that you created to hold the recordset.  
  
7.  On the Control Flow tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, add a Foreach Loop container and connect this container after the Data Flow task. Then, open the **Foreach Loop Editor** to configure the container with the following settings:  
  
    1.  On the **Collection** page, select the Foreach ADO Enumerator. Then, for **ADO object source variable**, select the variable that contains the recordset.  
  
    2.  On the **Variable Mappings** page, map the zero-based index of each column that you want to use to the appropriate variable.  
  
         On each iteration of the loop, the enumerator populates these variables with the column values from the current row.  
  
8.  Inside the Foreach Loop container, add and configure tasks to process one row of the recordset at a time by reading the values from the variables.  
  
## Example of Using the Recordset Destination  
 In the following example, the Data Flow task loads information about [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] employees from the Sales.SalesPerson table into a Recordset destination. Then, a Foreach Loop container reads one row of data at a time, and calls a Send Mail task. The Send Mail task uses expressions to send a customized e-mail message to each salesperson about the amount of his or her bonus.  
  
#### To create the project and configure the variables  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], create a new [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project.  
  
2.  On the **SSIS** menu, select **Variables**.  
  
3.  In the **Variables** window, create the variables that will hold the recordset and the column values from the current row:  
  
    1.  Create a variable named, `BonusRecordset`, and set its type to `Object`.  
  
         The `BonusRecordset` variable holds the recordset.  
  
    2.  Create a variable named, `EmailAddress`, and set its type to `String`.  
  
         The `EmailAddress` variable holds the salesperson's e-mail address.  
  
    3.  Create a variable named, `FirstName`, and set its type to `String`.  
  
         The `FirstName` variable holds the salesperson's first name.  
  
    4.  Create a variable named, `Bonus`, and set its type to `Double`.  
  
         The `Bonus` variable holds the amount of the salesperson's bonus.  
  
#### To configure the connection managers  
  
1.  In the Connection Managers area of the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, add and configure a new OLE DB connection manager that connects to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database.  
  
     The OLE DB source in the Data Flow task will use this connection manager to retrieve data.  
  
2.  In the Connection Managers area, add and configure a new SMTP connection manager that connects to an available SMTP server.  
  
     The Send Mail task inside the Foreach Loop container will use this connection manager to send emails.  
  
#### To configure the data flow and the Recordset Destination  
  
1.  On the **Control Flow** tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, add a Data Flow task to the design surface.  
  
2.  On the **Data Flow** tab, add an OLE DB source to the Data Flow task, and then open the **OLE DB Source Editor.**  
  
3.  On the **Connection Manager** page of the editor, configure the source with the following settings:  
  
    1.  For **OLE DB connection manager**, select the OLE DB connection manager that you previously created.  
  
    2.  For **Data access mode**, select **SQL command**.  
  
    3.  For **SQL command text**, enter the following query:  
  
        ```  
        SELECT     Person.Contact.EmailAddress, Person.Contact.FirstName, CONVERT(float, Sales.SalesPerson.Bonus) AS Bonus  
        FROM         Sales.SalesPerson INNER JOIN  
                              Person.Contact ON Sales.SalesPerson.SalesPersonID = Person.Contact.ContactID  
        ```  
  
        > [!NOTE]  
        >  You have to convert the `currency` value in the Bonus column to a `float` before you can load that value into a package variable whose type is `Double`.  
  
4.  On the **Data Flow** tab, add a Recordset destination, and connect the destination after the OLE DB source.  
  
5.  Open the **Recordset Destination Editor**, and configure the destination with the following settings:  
  
    1.  On the **Component Properties** tab, for `VariableName` property, select `User::BonusRecordset`.  
  
    2.  On the **Input Columns** tab, select all three of the available columns.  
  
#### To configure the Foreach Loop container and run the package  
  
1.  On the **Control Flow** tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, add a Foreach Loop container, and connect the container after the Data Flow task.  
  
2.  Open the **Foreach Loop Editor**, and configure the container with the following settings:  
  
    1.  On the **Collection** page, for **Enumerator**, select **Foreach ADO Enumerator**, and for **ADO object source variable**, select `User::BonusRecordset`.  
  
    2.  On the **Variable Mappings** page, map `User::EmailAddress` to index 0, `User::FirstName` to index 1, and `User::Bonus` to index 2.  
  
3.  On the **Control Flow** tab, inside the Foreach Loop container, add a Send Mail task.  
  
4.  Open the **Send Mail Task Editor**, and then on the **Mail** page, configure the task with the following settings:  
  
    1.  For **SmtpConnection**, select the SMTP connection manager that was configured previously.  
  
    2.  For **From**, enter an appropriate e-mail address.  
  
         If you use your own e-mail address, you will be able to confirm that the package runs successfully. You will receive undeliverable receipts for the messages sent by the Send Mail task to the fictitious salespersons of [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)].  
  
    3.  For **To**, enter a default e-mail address.  
  
         This value will not be used, but will be replaced at run time by the e-mail address of each salesperson.  
  
    4.  For **Subject**, enter "Your annual bonus".  
  
    5.  For **MessageSourceType**, select **Direct Input**.  
  
5.  On the **Expressions** page of the **Send Mail Task Editor**, click the ellipsis button (**...**) to open the **Property Expressions Editor**.  
  
6.  In the **Property Expressions Editor**, enter the following information:  
  
    1.  For **ToLine**, add the following expression:  
  
        ```  
        @[User::EmailAddress]  
        ```  
  
    2.  For the **MessageSource** property, add the following expression:  
  
        ```  
        "Dear " +  @[User::FirstName] + ": The amount of your bonus for this year is $" +  (DT_WSTR, 12) @[User::Bonus] + ". Thank you!"  
        ```  
  
7.  Run the package.  
  
     If you have specified a valid SMTP server and provided your own e-mail address, you will receive undeliverable receipts for the messages that the Send Mail task sends to the fictitious salespersons of [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)].  
  
  
