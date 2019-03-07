---
title: SQL Data Discovery & Classification | Microsoft Docs
description: SQL Data Discovery & Classification
documentationcenter: ''
ms.reviewer: carlrab
ms.assetid: 89c2a155-c2fb-4b67-bc19-9b4e03c6d3bc
ms.service: sql-database
ms.prod_service: sql-database,sql
ms.custom: security
ms.topic: conceptual
ms.date: 02/13/2018
ms.author: giladm
author: giladm
manager: shaik
---
# SQL Data Discovery and Classification
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Data Discovery & Classification introduces a new tool built into SQL Server Management Studio (SSMS) for **discovering**, **classifying**, **labeling** & **reporting** the sensitive data in your databases.
Discovering and classifying your most sensitive data (business, financial, healthcare, etc.) can play a pivotal role in your organizational information protection stature. It can serve as infrastructure for:
* Helping meet data privacy standards.
* Controlling access to and hardening the security of databases/columns containing highly sensitive data.

> [!NOTE]
> Data Discovery & Classification is **supported for SQL Server 2008 and later**. For Azure SQL Database, see [Azure SQL Database Data Discovery & Classification](https://go.microsoft.com/fwlink/?linkid=866265).

## <a id="subheading-1"></a>Overview
Data Discovery & Classification introduces a set of advanced services, forming a new SQL Information Protection paradigm aimed at protecting the data, not just the database:
* **Discovery & recommendations** - The classification engine scans your database and identifies columns containing potentially sensitive data. It then provides you an easy way to review and apply the appropriate classification recommendations, as well as to manually classify columns.
* **Labeling** - Sensitivity classification labels can be persistently tagged on columns.
* **Visibility** - The database classification state can be viewed in a detailed report that can be printed/exported to be used for compliance & auditing purposes, as well as other needs.

## <a id="subheading-2"></a>Discovering, classifying & labeling sensitive columns
The following section describes the steps for discovering, classifying, and labeling columns containing sensitive data in your database, as well as viewing the current classification state of your database and exporting reports.

The classification includes two metadata attributes:
* Labels - The main classification attributes, used to define the sensitivity level of the data stored in the column.  
* Information Types - Provide additional granularity into the type of data stored in the column.

<br>
**To classify your SQL Server database:**

1. In SQL Server Management Studio (SSMS) connect to the SQL Server.

2. In the SSMS Object Explorer, right click on the database that you would like to classify and choose **Tasks** > **Classify Data...**.

    ![Navigation pane][1]

3. The classification engine scans your database for columns containing potentially sensitive data and provides a list of **recommended column classifications**:

    * To view the list of recommended column classifications, click on the recommendations notification box at the top or the recommendations panel at the bottom of the window:

        ![Navigation pane][2]

        ![Navigation pane][3]

    * Review the list of recommendations:
        * To accept a recommendation for a specific column, check the checkbox in the left column of the relevant row. You can also mark *all recommendations* as accepted by checking the checkbox in the recommendations table header.

        * You can also change the recommended Information Type and Sensitivity Label using the drop down boxes.        

        ![Navigation pane][4]

    * To apply the selected recommendations, click on the blue **Accept selected recommendations** button.

        ![Navigation pane][5]

4. You can also **manually classify** columns as an alternative, or in addition, to the recommendation-based classification:

    * Click on **Add classification** in the top menu of the window.

        ![Navigation pane][6]

    * In the context window that opens, select the schema > table > column that you want to classify, and the information type and sensitivity label. Then click on the blue **Add classification** button at the bottom of the context window.

        ![Navigation pane][7]

5. To complete your classification and persistently label (tag) the database columns with the new classification metadata, click on **Save** in the top menu of the window.

    ![Navigation pane][8]


6. To generate a report with a full summary of the database classification state, click on **View Report** in the top menu of the window.

    ![Navigation pane][9]

    ![Navigation pane][10]


## <a id="subheading-3"></a>Accessing the classification metadata

The classification metadata for *Information Types* and *Sensitivity Labels* is stored in the following Extended Properties: 
* sys_information_type_name
* sys_sensitivity_label_name

The metadata can be accessed using the Extended Properties catalog view [sys.extended_properties](https://docs.microsoft.com/sql/relational-databases/system-catalog-views/extended-properties-catalog-views-sys-extended-properties).

The following code example returns all classified columns with their corresponding classifications:

```sql
SELECT
    schema_name(O.schema_id) AS schema_name,
    O.NAME AS table_name,
    C.NAME AS column_name,
    information_type,
    sensitivity_label 
FROM
    (
        SELECT
            IT.major_id,
            IT.minor_id,
            IT.information_type,
            L.sensitivity_label 
        FROM
        (
            SELECT
                major_id,
                minor_id,
                value AS information_type 
            FROM sys.extended_properties 
            WHERE NAME = 'sys_information_type_name'
        ) IT 
        FULL OUTER JOIN
        (
            SELECT
                major_id,
                minor_id,
                value AS sensitivity_label 
            FROM sys.extended_properties 
            WHERE NAME = 'sys_sensitivity_label_name'
        ) L 
        ON IT.major_id = L.major_id AND IT.minor_id = L.minor_id
    ) EP
    JOIN sys.objects O
    ON  EP.major_id = O.object_id 
    JOIN sys.columns C 
    ON  EP.major_id = C.object_id AND EP.minor_id = C.column_id
```

## <a id="subheading-4"></a>Next steps

For Azure SQL Database, see [Azure SQL Database Data Discovery & Classification](https://go.microsoft.com/fwlink/?linkid=866265).

Consider protecting your sensitive columns by applying column level security mechanisms:

* [Dynamic Data Masking](https://docs.microsoft.com/sql/relational-databases/security/dynamic-data-masking) for obfuscating sensitive columns in use.
* [Always Encrypted](https://docs.microsoft.com/sql/relational-databases/security/encryption/always-encrypted-database-engine) for encrypting sensitive columns at rest.

<!--Anchors-->
[SQL Data Discovery & Classification overview]: #subheading-1
[Discovering, classifying & labeling sensitive columns]: #subheading-2
[Accessing the classification metadata]: #subheading-3
[Next Steps]: #subheading-4

<!--Image references-->
[1]: ./media/sql-data-discovery-and-classification/1_data_classification_explorer_menu.png
[2]: ./media/sql-data-discovery-and-classification/2_recommendations_notification_box.png
[3]: ./media/sql-data-discovery-and-classification/3_recommendations_panel.png
[4]: ./media/sql-data-discovery-and-classification/4_recommendations.png
[5]: ./media/sql-data-discovery-and-classification/5_accept_recommendations_button.png
[6]: ./media/sql-data-discovery-and-classification/6_add_classification_button.png
[7]: ./media/sql-data-discovery-and-classification/7_manual_classification.png
[8]: ./media/sql-data-discovery-and-classification/8_save.png
[9]: ./media/sql-data-discovery-and-classification/9_view_report.png
[10]: ./media/sql-data-discovery-and-classification/10_report.png
