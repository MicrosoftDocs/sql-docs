---
title: Database Migration Assessment for Oracle extension
description: Learn how to install the Azure Data Studio Database Migration Assessment for Oracle extension. This extension helps guide you choose the best Azure  SQL database and Azure database for PostgreSQL migration path.
author: markingmyname
ms.author: maghan
ms.reviewer: niball
ms.date: 04/27/2022
ms.prod: azure-data-studio
ms.topic: conceptual
---

# Database Migration Assessment for Oracle extension (Preview)

The Database Migration Assessment for Oracle extension in Azure Data Studio helps you assess an Oracle workload for migrating to Azure SQL and Azure database for PostgreSQL. The extension identifies an appropriate Azure SQL or PostgreSQL target with right-sizing recommendations, and how complex the migration can be.

>[!Note]
> Try the latest Oracle to Azure database for PostgreSQL workload and code assessment.

## Pre-requisites

- An [Oracle database](https://www.oracle.com/database/technologies/oracle-database-software-downloads.html) running version 10 g or higher.
- [Azure Data Studio version 1.39  or above](../download-azure-data-studio.md).
- Oracle user should have CONNECT and SELECT ANY DICTIONARY privileges assigned.

## Install Azure Data Studio extension

Follow the steps below to install the Database Migration Assessment for Oracle extension in Azure Data Studio.

1. Open the extensions manager in Azure Data Studio. You can select the extensions icon or select **Extensions** in the View menu.

2. Type *Oracle* in the search bar.

3. Select the **Database Migration Assessment for Oracle** extension and view its details.

4. Select **Install**.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/install-database-migration-assessment-for-oracle-extension.png" alt-text="install extension":::

## Configure extension settings

There are few extension settings that can be configured after installing the extension or updated later.

1. Go to extensions and select **Database Migration Assessment for Oracle** , click on manage settings icon and then select extension settings.

     :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmaof-extension-manage.png" alt-text="extension manage settings":::
    
2. Under extension settings, edit any extension settings to meet the environment's requirement.

     :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmaof-extension-settings-edit.png" alt-text="extension properties settings":::
    
    >[!Note]
    > To perform Oracle to PostgreSQL code assessment, Oracle client home path and Ora2pg installation path are mandatory parameters.
    
## Run assessment

Once the assessment extension installs, the next step is to connect to Oracle your database, collect metadata information from your Oracle instance and generate an assessment report.

1. Go to the connections icon in the menu bar.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-connections-menu-bar.png" alt-text="connections menu bar":::

2. In the **Connection Details**, fill out the fields.
    1. In **Connection type** field, select **Oracle**.
    2. In the **Data Source** field, type in your Oracle server name and instance.
        1. You can provide the TNS name as well. (Make sure that ```ORACLE_HOME``` environment variable is set and TNSNAMES.ORA file is located in the ```<ORACLE_HOME>/network/admin folder```. )
    3. In the **User Id** field, provide the database username.
    4. In the **Password** field, provide the database password.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-connections-details.png" alt-text="connection details":::
3. Select **Connect**.

4. Now, a new connection appears in the connection details menu.

5. Right-click on the Oracle connection and select **Manage**.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-manage-database-connection.png" alt-text="manage database":::

6. Select **Migration Assessment**.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-select-oracle-assessment.png" alt-text="migration assessment database  ":::

7. Select **Run new Assessment**.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-run-new-oracle-assessment.png" alt-text="run new assessment":::

8. Provide your assessment details.
    1. In the **Assessment name** field, enter a title.
        1. For example, *demo1*.
    1. Enter your **Assessment setting**.
        1. In the **Target Platform** field, enter the destination migration database.Currently, it supports SQL and PostgreSQL as target.
        1. In the **Performance data collection** section, either select run performance data collection on Oracle database (connected) or add AWR report.
        The recommendation is to use connected option if you are running this tool during peak or realistic load. Otherwise, provide the AWR reports generated in past for performance and sizing recommendation.
        1. In the **Scale factor** field, enter the multiplier value.
            1. If the recommended SKU needs to consider other peak load, the scale factor multiplier should be greater than 1. Example: Burst Load, Seasonal usage, future capacity planning etc. Whereas when  partial Oracle schema workload is considered  migrated, then the multiplier should be less than 1.

        1. In the **Percentile utilization** field, enter the percentile value for sizing the Azure target.

            1. The percentile value of the performance sample set to be considered for sizing the Azure target.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-assessment-details.png" alt-text="assessment details":::

9. When the target is PostgreSQL, there are two types of assessment

    - Workload Assessment - In this assessment, the Oracle assessment module performs a lightweight discovery of the schema objects and categorize the schema complexity for migration to various categories with a high level estimated conversion hours.

    - Code Assessment - The code assessment performs a deep assessment of the Oracle schema objects and then suggests an overall readiness of the code objects with ready, need review and not ready object types at granular level and its associated conversion hours with higher accuracy.

        >[!Note]
        > For code assessment, the extension uses open source Ora2pg schema convertor.
    
    If  Oracle client and\or Ora2pg is not installed in the Azure data studio tool machine, either manually install them or use the script available here. - [Ora2pg installation script](https://github.com/microsoft/OrcasNinjaTeam/tree/master/ora2pg-install)

    If Oracle client and Ora2pg are installed, ensure that the Oracle client home path and Ora2pg installation path are updated in the extension settings.

10. Now, you see the new assessment in the last five assessments section.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-demo.png" alt-text="name the assessment demo":::

11. On Click on the assessment link to view assessment details page. You can view the latest assessment status.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-latest-assessment-status.png" alt-text="latest assessment status":::

12. Click refresh or wait until the Assessment completes. The assessment status page refreshes frequently. The default value is 15 seconds. The status updates to show you 1 of 4 statuses - **success**, **failed** , **in-progress** or **canceled**.

## View assessment

Once the Assessment is complete, a consolidated output is generated for either each Azure SQL target:  **SQL Server on Azure Virtual Machines**, **Azure SQL Database**, and **Azure SQL Managed Instance** or **Azure database for PostgreSQL - Flexible server**.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-view-assessment.png" alt-text="view assessment":::

Each card has multiple sections. The card shows the overall feasibility of the migration to the  target.  Also drills down on various workloads under various migration complexity categories. The feature compatibility section that provides the feature assessment review result. SKU recommendation provides the proper sizing of the target. The code assessment provides the code complexity of database objects.

When you click on view detail report, it first shows the summary of the Assessment.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-card-sections.png" alt-text="card sections":::

If the target selected is Database for Azure PostgreSQL, either database details tab or Code assessment tab will be visible, based on the assessment type selection.

The following database details tab provides the breakup per schema basis. It shows the list of schemas are discovered, the migration feasibility, and the estimated time to convert schema in hours.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-per-schema-basis.png" alt-text="per schema basis":::

This estimation is based on a statistical model that applies to the object count, lines of code, enabled features and size of the database. For more accurate estimate on the code conversion, use [SQL Server migration Assistant for Oracle](../../ssma/oracle/sql-server-migration-assistant-for-oracle-oracletosql.md).

>[!Note]
>The following system or Oracle built-in schemas are excluded in the workload assessment
'SYSTEM','CTXSYS','DBSNMP','EXFSYS','LBACSYS','MDSYS','MGMT_VIEW' 'OLAPSYS','ORDDATA','OWBSYS','ORDPLUGINS','ORDSYS','OUTLN','SI_INFORMTN_SCHEMA','SYS 'SYSMAN','WK_TEST','WKSYS','WKPROXY','WMSYS','XDB','DIP','MDDATA','ORACLE_OCM', 'SPATIAL_CSW_ADMIN_USR','SPATIAL_WFS_ADMIN_USR','XS$NULL','PERFSTAT','SQLTXPLAIN','DMSYS','TSMSYS','WKSYS','DVSYS','OJVMSYS','GSMADMIN_INTERNAL','APPQOSSYS','DVSYS','DVF','AUDSYS','MGMT_VIEW','ODM','ODM_MTR','TRACESRV','MTMSYS','OWBSYS_AUDIT','WEBSYS','WK_PROXY','OSE$HTTP$ADMIN','DBMS_PRIVILEGE_CAPTURE','CSMIG','MGDSYS','SDE','DBSFWUSER','APEX','FLOW_'

The SKU recommendation provides the suitable Azure target, its service tier and the metric thresholds that have been used to provide the recommended SKU.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-skus.png" alt-text="Sku recommendations":::

The SKU  recommendation evaluates various performance metrics - CPU, memory, IOPS, latency and storage. Based on the usage and the configuration data, the recommender provides the suitable target and the appropriate service tier.

> [!Note]
> If automatic workload repository (AWR) feature is enabled at Oracle instance, then SKU recommender will use the DBA_HIST_ views to gather the performance metrics metadata. Otherwise, the recommender will use server configuration and other system views information for sizing the Azure SQL target.

The code compatibility (_PostgreSQL target only_) provides a summary of schema objects that can be converted to Azure target. The report provides the breakup of the objects that can be converted automatically, ready with conditions and need to be remediated and converted manually.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmao-code-compatibility-pg.png" alt-text="code compatibility report":::

The report shows the total number of objects and  conversion effort hours required for migrating code to database for Azure PostgreSQL. The graphical image provides the breakup for each schema object type such as tables, views, cluster etc. The efforts are categorized as Ready - Automatically converted, Needs review and not ready - requires manual conversion.

There is a drill-down for not ready and need review section. It provides the list of remediation tasks and the  effort hours for each task.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmao-remediation-pg.png" alt-text="remediation task":::


The feature assessment provides the Oracle to Azure data target mapped features and the effort required for migrating those capabilities to Azure  target.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-mapped-assessment.png" alt-text="Mapped assessment":::

You can download the report for offline viewing by selecting download combined reports or download individual reports.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-downlaod-report.png" alt-text="download assessment report":::

You can also cancel an ongoing assessment, delete an assessment and move assessments to another directory.

## Change Assessment Directory

1. Go to extension marketplace and search for Database Migration Assessment for Oracle.
1. Click on the manage icon and click extension settings.
1. Provide the new assessment path under Oracle Assessment: Assessment Path.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-extension.png" alt-text="Screen capture shows the Change assessment path":::

## Troubleshoot

### Logging

The extension has the errors, warning and other diagnostic logging written in the default log directory.

- Windows - C:\Users\\<username\>\\.dmaoracle\logs\
- Linux  - ~/.dmaoracle/logs
- Mac  - /Users/\<username\>/.dmaoracle/logs

By default, the extension stores last seven log files.

To change the log directory, update LogDirectory property in the extension settings file.

Windows - C:\Users\\<username\>\\.azuredatastudio\extensions\microsoft.azuredatastudio-dma-oracle-\<VersionNumber\>\bin\service\Properties\ConfigSettings\extension-settings.json

Linux  - ~/.azuredatastudio/extensions/microsoft.azuredatastudio-dma-oracle-/\<VersionNumber\>/bin/service/Properties/ConfigSettings/extension-settings.json

Mac  - /Users/\<username\>/.azuredatastudio/extensions/microsoft.azuredatastudio-dma-oracle-/\<VersionNumber\>/bin/service/Properties/ConfigSettings/extension-settings.json

### Known issues

- Path provided doesn't exist.

    Reason: Missing files or missing permission on the assessment folder.

   Possible Solution:

    a. User has read and write permission on the assessment folder.

    b. If there's a missing file or folder, delete the assessment and generate a new assessment.

- Encountered connection timeout exception while interacting with Oracle.

     Reason: Failed to Connect to Oracle Instance.

    Possible Solution:

    a. Check if Oracle listening port is blocked by firewall rules.

    b. Perform tnsping and see if the Service ID gets resolved.

## Next steps

- [Azure SQL Migration extension](azure-sql-migration-extension.md)
- [Oracle extension](extension-for-oracle.md)
- [Data Schema Conversion](dsct/database-schema-conversion-toolkit.md)
- [Add extensions](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/xml-bulk-load-examples-sqlxml-4-0.md)
