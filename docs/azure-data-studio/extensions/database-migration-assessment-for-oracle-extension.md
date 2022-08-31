---
title: Database Migration Assessment for Oracle extension
description: Learn how to install the Azure Data Studio Database Migration Assessment for Oracle extension. This extension helps guide you choose the best Azure SQL database migration path.
author: markingmyname
ms.author: maghan
ms.reviewer: niball
ms.date: 04/27/2022
ms.prod: azure-data-studio
ms.topic: conceptual
---

# Database Migration Assessment for Oracle extension (Preview)

The Database Migration Assessment for Oracle extension in Azure Data Studio helps you assess an Oracle workload for migrating to SQL. The extension identifies an appropriate Azure SQL target with right-sizing recommendations, and how complex the migration can be.

## Pre-requisites

- An [Oracle database](https://www.oracle.com/database/technologies/oracle-database-software-downloads.html) running version 10 g or higher.
- [Azure Data Studio version 1.37  or above](../download-azure-data-studio.md).
- Oracle user should have CONNECT and SELECT ANY DICTIONARY privileges assigned.

## Install Azure Data Studio extension

Follow the steps below to install the Database Migration Assessment for Oracle extension in Azure Data Studio.

1. Open the extensions manager in Azure Data Studio. You can select the extensions icon or select **Extensions** in the View menu.

2. Type *Oracle* in the search bar.

3. Select the **Database Migration Assessment for Oracle** extension and view its details.

4. Select **Install**.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/install-database-migration-assessment-for-oracle-extension.png" alt-text="install extension":::

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
        1. In the **Target Platform** field, enter the destination migration database.
            1. For example, **SQL**.
        1. In the **Scale factor** field, enter the multiplier value.
            1. If the recommended SKU needs to consider other peak load, the scale factor multiplier should be greater than 1. Example: Burst Load, Seasonal usage, future capacity planning etc. Whereas, when  partial Oracle schema workload is considered  migrated, then the multiplier should be less than 1.

        1. In the **Percentile utilization** field, enter the percentile value for sizing the Azure target.

            1. The percentile value of the performance sample set to be considered for sizing the Azure target.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-assessment-details.png" alt-text="assessment details":::

9. Now, you see the new assessment in the last five assessments section.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-demo.png" alt-text="name the assessment demo1":::

10. On Click on the assessment link to view assessment details page. You can view the latest assessment status.

    :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-latest-assessment-status.png" alt-text="latest assessment status":::

11. Click refresh or wait until the Assessment completes. The assessment status page refreshes frequently. The default value is 15 seconds. The status updates to show you 1 of 4 statuses - **success**, **failed** , **in-progress** or **canceled**.

## View assessment

Once the Assessment is complete, a consolidated output is generated for each Azure SQL target. Currently, these targets include **SQL Server on Azure Virtual Machines**, **Azure SQL Database**, and **Azure SQL Managed Instance**.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-view-assessment.png" alt-text="view assessment":::

Each card has three sections. The card shows the overall feasibility of the migration to the SQL target.  Also drills down on various workloads under various migration complexity categories. The feature compatibility section that provides the feature assessment review result. SKU recommendation provides the proper sizing of the target.

When you click on view detail report, it first shows the summary of the Assessment.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-card-sections.png" alt-text="card sections":::

The following database details tab provides the breakup per schema basis. It shows the list of schemas are discovered, the migration feasibility, and the estimated time to convert schema in hours.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-per-schema-basis.png" alt-text="per schema basis":::

This estimation is based on a statistical model that applies to the object count, lines of code, enabled features and size of the database. For more accurate estimate on the code conversion, use [SQL Server migration Assistant for Oracle](../../ssma/oracle/sql-server-migration-assistant-for-oracle-oracletosql.md).

>[!Note]
>The following system or Oracle built-in schemas are excluded in the workload assessment
'SYSTEM','CTXSYS','DBSNMP','EXFSYS','LBACSYS','MDSYS','MGMT_VIEW' 'OLAPSYS','ORDDATA','OWBSYS','ORDPLUGINS','ORDSYS','OUTLN','SI_INFORMTN_SCHEMA','SYS 'SYSMAN','WK_TEST','WKSYS','WKPROXY','WMSYS','XDB','DIP','MDDATA','ORACLE_OCM', 'SPATIAL_CSW_ADMIN_USR','SPATIAL_WFS_ADMIN_USR','XS$NULL','PERFSTAT','SQLTXPLAIN','DMSYS','TSMSYS','WKSYS','DVSYS','OJVMSYS','GSMADMIN_INTERNAL','APPQOSSYS','DVSYS','DVF','AUDSYS','MGMT_VIEW','ODM','ODM_MTR','TRACESRV','MTMSYS','OWBSYS_AUDIT','WEBSYS','WK_PROXY','OSE$HTTP$ADMIN','DBMS_PRIVILEGE_CAPTURE','CSMIG','MGDSYS','SDE','DBSFWUSER','APEX','FLOW_'

The SKU recommendation provides the suitable Azure SQL target, its service tier and the metric thresholds that have been used to provide the recommended SKU.

:::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-skus.png" alt-text="Sku recommendations":::

The SKU  recommendation evaluates various performance metrics - CPU, memory, IOPS, latency and storage. Based on the usage and the configuration data, the recommender provides the suitable SQL target and the appropriate service tier.

> [!Note]
> If automatic workload repository (AWR) feature is enabled at Oracle instance, then SKU recommender will use the DBA_HIST_ views to gather the performance metrics metadata. Otherwise, the recommender will use server configuration and other system views information for sizing the Azure SQL target.

The feature assessment provides the Oracle to Azure SQL mapped features and the effort required for migrating those capabilities to Azure SQL target.

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

By default, the extension stores last 7 log files.

To change the log directory, update LogDirectory property in the extension settings file.

Windows - C:\Users\\<username\>\\.azuredatastudio\extensions\microsoft.azuredatastudio-dma-oracle-\<VersionNumber\>\bin\service\Properties\ConfigSettings\extension-settings.json

Linux  - ~/.azuredatastudio/extensions/microsoft.azuredatastudio-dma-oracle-/\<VersionNumber\>/bin/service/Properties/ConfigSettings/extension-settings.json

Mac  - /Users/\<username\>/.azuredatastudio/extensions/microsoft.azuredatastudio-dma-oracle-/\<VersionNumber\>/bin/service/Properties/ConfigSettings/extension-settings.json

### Known issues

- Path provided does not exist.

    Reason: Missing files or missing permission on the assessment folder.

   Possible Solution:

    a. User has read and write permission on the assessment folder.

    b. If there is a missing file or folder, delete the assessment and generate a new assessment.

- Encountered connection timeout exception while interacting with Oracle.

     Reason: Failed to Connect to Oracle Instance.

    Possible Solution:

    a. Check if the port Oracle is running on is blocked by firewall rules.

    b. Perform tnsping and see if the Service ID gets resolved.

- Unable to access Database Migration Assessment reports for Oracle after upgrading to version 1.5

    Reason: When Azure data studio Oracle extension is upgraded to latest version (version 1.5 and above), the assessment reports will be inaccessible.

    Workaround:  Uninstall and install the prior version of Database migration assessment (v1.4.1)  to download the previously assessed reports. Please follow the steps below to use previous version of the extension

    1. Pre-requisites

        • Current assessment Directory: Path is given under Extension settings > Oracle Assessment: Assessment Path > Local path to store your assessments.

        • Previous version build: Download the Previous build (version 1.4.1) on local system.

    1. Troubleshooting steps

        • Open the extensions manager in Azure Data Studio. You can select the extensions icon or select Extensions in the View menu.

        • To Install, click on View and More Actions (3 dots) as shown below > Install from VSIX.
     Screen capture shows view and More Actions to chose Install from vsix.

        :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-install-from-vsix.png" alt-text="Screen capture shows Veiw and More Actions to chose Install from vsix":::

       •  Locate the VSIX file of v1.4.1 saved locally and click Install.

        :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-select-vsix.png" alt-text="Screen capture shows selecting local vsix.":::

        • Close and reopen the Azure data studio.

        Ensure to set the extension assessment directory same as noted in pre-requisites.

        • Using this extension version v1.4.1, saved assessments reports can be accessed, downloaded and saved in HTML format.

## Next steps

- [Azure SQL Migration extension](azure-sql-migration-extension.md)
- [Oracle extension](extension-for-oracle.md)
- [Data Schema Conversion](dsct/database-schema-conversion-toolkit.md)
- [Add extensions](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/xml-bulk-load-examples-sqlxml-4-0.md)
