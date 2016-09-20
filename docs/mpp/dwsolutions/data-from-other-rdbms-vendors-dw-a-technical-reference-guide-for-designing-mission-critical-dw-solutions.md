---
title: "Data from Other RDBMS Vendors (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: da6f9933-c989-411c-8538-e0e93c19c78c
caps.latest.revision: 3
manager: jeffreyg
---
# Data from Other RDBMS Vendors (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
  <introduction>
    <table xmlns:caps="http://schemas.microsoft.com/build/caps/2013/11">
      <tbody>
        <tr>
          <TD>
            <para>
              <embeddedLabel>Want more guides like this one?</embeddedLabel> Go to <externalLink><linkText>Technical Reference Guides for Designing Mission-Critical Solutions</linkText><linkUri>http://technet.microsoft.com/en-us/sqlserver/hh273157</linkUri></externalLink>.</para>
          </TD>
        </tr>
      </tbody>
    </table>
    <para>It is not uncommon for one or more other database platforms to serve as the source of both transactional (OLTP) data as well as from other data warehouses. Key distinguishing features primarily focus on the connectivity to the other database system drivers, as well as the geographical proximity of the other database (DB) server source to the data warehouse target. Other important items include volume, frequency of extract/load, complexity of data transformation, access windows to extract data. </para>
    <para>The primary RDBMS vendors include IBM (DB2), Oracle, Sybase, and Teradata.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>This section provides some best practice guidance and resources for more information.</para>
      <list class="bullet">
        <listItem>
          <para>Attunity drivers for Oracle and Teradata:<externalLink><linkText>Attunity Delivers New Oracle and Teradata Connectors for Microsoft SQL Server 2008</linkText><linkUri>http://www.attunity.com/press_releases.aspx?newsId=1200</linkUri></externalLink><superscript>1</superscript> and <externalLink><linkText>Microsoft Download Center: Microsoft Connectors for Oracle and Teradata by Attunity</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=d9cb21fe-32e9-4d34-a381-6f9231d84f1e&amp;displaylang=en</linkUri></externalLink><superscript>2</superscript></para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>ODBC FAQ</linkText>
              <linkUri>http://www.orafaq.com/wiki/ODBC_FAQ</linkUri>
            </externalLink>
            <superscript>3</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Using the JDBC Connectivity Layer in Oracle Warehouse Builder</linkText>
              <linkUri>http://www.oracle.com/technetwork/articles/datawarehouse/vasiliev-owb-jdbc-183946.html</linkUri>
            </externalLink>
            <superscript>4</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Moving Large Amounts of Data Between Oracle and SQL Server: Findings and Observations</linkText>
              <linkUri>http://sqlcat.com/technicalnotes/archive/2008/08/09/moving-large-amounts-of-data-between-oracle-and-sql-server-findings-and-observations.aspx</linkUri>
            </externalLink>"<superscript>5</superscript></para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>EMC Consulting Blog</linkText>
              <linkUri>http://consultingblogs.emc.com/jamiethomson/archive/2008/03/14/ssis-support-for-oracle-just-got-a-whole-lot-better.aspx</linkUri>
            </externalLink>
            <superscript>6</superscript>
          </para>
        </listItem>
        <listItem>
          <para>IBM (DB2)-Related: Often DB2 sources require DB2 version specific drivers for SSIS to connect to DB2 source. Consult relevant owners of DB2 source systems, obtain appropriate drivers and have them installed in all environments SSIS will be executed in (Development, Test, Production).</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>IBM DB2 Driver for ODBC and CLI Overview</linkText>
              <linkUri>http://publib.boulder.ibm.com/infocenter/db2luw/v9/index.jsp?topic=/com.ibm.db2.udb.apdv.cli.doc/doc/c0023378.htm</linkUri>
            </externalLink>
            <superscript>7</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>An Overview of DB2 and Java DataBase Connectivity (JDBC)</linkText>
              <linkUri>http://www.ibm.com/developerworks/data/library/techarticle/0203zikopoulos/0203zikopoulos.html</linkUri>
            </externalLink>
            <superscript>8</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Connecting to DB2 from SSIS</linkText>
              <linkUri>http://geekswithblogs.net/13DaysaWeek/archive/2010/05/16/connecting-to-db2-from-ssis.aspx</linkUri>
            </externalLink>
            <superscript>9</superscript>
          </para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>The following case studies can be used for reference.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>SSIS and Data Sources</linkText>
              <linkUri>http://social.technet.microsoft.com/wiki/contents/articles/ssis-and-data-sources.aspx</linkUri>
            </externalLink>
            <superscript>10</superscript> contains details on various sources for SSIS. </para>
        </listItem>
        <listItem>
          <para>Detailed explanation for handling File sources are included in Chapter 4, starting on page 97 in SQL Server 2008 Integration Services Problem—Design—Solution.</para>
        </listItem>
        <listItem>
          <para>SQL Server has been deployed for many Tier-1 and lower Tier applications by customers. Examples include: Barnes and Noble, Hilton Hotels, Stein Mart.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>Become familiar with other DBMS Connectivity Requirements</para>
        </listItem>
        <listItem>
          <para>Determine all connectivity requirements (e.g. connection managers, OLEDB, ODBC, JDBC, and so on).</para>
        </listItem>
        <listItem>
          <para>Understand source database design (Star Schema, normalized).</para>
        </listItem>
        <listItem>
          <para>Understand if the client will rely on Flat Files versus ETL/ELT (e.g. SSIS).</para>
        </listItem>
        <listItem>
          <para>For Flat Files:</para>
          <list class="bullet">
            <listItem>
              <para>File structure - Fixed-length</para>
            </listItem>
            <listItem>
              <para>File structure - Multi-level/records with record-type indicator</para>
            </listItem>
            <listItem>
              <para>File structure - Variable-length, delimited</para>
            </listItem>
            <listItem>
              <para>File structure - End of Record (EOR) Delimiter</para>
            </listItem>
            <listItem>
              <para>File structure - End of File (EOF) Marker</para>
            </listItem>
            <listItem>
              <para>Mapping requirements to external SAN Storage</para>
            </listItem>
            <listItem>
              <para>File storage platform and architecture</para>
            </listItem>
            <listItem>
              <para>Can extracts be multi-threaded?</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Understand Data type Mappings between source/target columns.</para>
        </listItem>
        <listItem>
          <para>Determine ETL/ELT requirements with regards to:</para>
          <list class="bullet">
            <listItem>
              <para>Data volume</para>
            </listItem>
            <listItem>
              <para>Data "latency" or timing (how frequently does data need to be loaded)</para>
            </listItem>
            <listItem>
              <para>Transformation complexity (from simple table lookups to complex business-oriented /data enrichment transforms)</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Determine geographical proximity of source and target systems. If there are issues related to network infrastructure and size of source data consider the following strategy:</para>
          <list class="bullet">
            <listItem>
              <para>Extract Source Data (from Oracle, Teradata, or another provider), to a .txt (preferably pipe (|) delimited file),.</para>
            </listItem>
            <listItem>
              <para>Move this file to disc storage local to destination SQL Server.</para>
            </listItem>
            <listItem>
              <para>Load data from flat file to SQL Server.</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Understand characteristics of the target (is it another SQL Server instance, or a flat file, or another RDBMS or analytical platform (e.g. SSAS)?</para>
        </listItem>
        <listItem>
          <para>Establish strategy for archiving source data from external sources. Often source data is only available for a predefined window of time slotted for ETL. This doesn’t provide for possible data re-loading and auditing purposes. Archive source data either by storing flat files that source data was extracted to, or by archiving landing databases containing unchanged source data is recommended practice.</para>
        </listItem>
        <listItem>
          <para>Custom SSIS connection managers and tasks can be obtained from trusted third-party providers. One of the examples is SFTP connection manager. Rather than scripting access to secure FTP consider using reliable third-party components as <externalLink><linkText>SFTP Task</linkText><linkUri>http://www.cozyroc.com/ssis/sftp-task</linkUri></externalLink>.<superscript>11</superscript></para>
        </listItem>
        <listItem>
          <para />
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Attunity Delivers New Oracle and Teradata Connectors for Microsoft SQL Server 2008 <externalLink><linkText>http://www.attunity.com/press_releases.aspx?newsId=1200</linkText><linkUri>http://www.attunity.com/press_releases.aspx?newsId=1200</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Microsoft Download Center: Microsoft Connectors for Oracle and Teradata by Attunity  <externalLink><linkText>http:/</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=d9cb21fe-32e9-4d34-a381-6f9231d84f1e&amp;displaylang=en</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> ODBC FAQ  http://<externalLink><linkText>www.orafaq.com/wiki/ODBC_FAQ</linkText><linkUri>http://www.orafaq.com/wiki/ODBC_FAQ</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Using the JDBC Connectivity Layer in Oracle Warehouse Builder  <externalLink><linkText>http://www.oracle.com/technetwork/articles/datawarehouse/vasiliev-owb-jdbc-183946.html</linkText><linkUri>http://www.oracle.com/technetwork/articles/datawarehouse/vasiliev-owb-jdbc-183946.html</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Moving Large Amounts of Data Between Oracle and SQL Server: Findings and Observations  <externalLink><linkText>http://</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2008/08/09/moving-large-amounts-of-data-between-oracle-and-sql-server-findings-and-observations.aspx</linkUri></externalLink><externalLink><linkText>sqlcat.com/technicalnotes/archive/2008/08/09/moving-large-amounts-of-data-between-oracle-and-sql-server-findings-and-observations.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2008/08/09/moving-large-amounts-of-data-between-oracle-and-sql-server-findings-and-observations.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> EMC Consulting Blog  <externalLink><linkText>http://consultingblogs.emc.com/</linkText><linkUri>http://consultingblogs.emc.com/jamiethomson/archive/2008/03/14/ssis-support-for-oracle-just-got-a-whole-lot-better.aspx</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> IBM DB2 Driver for ODBC and CLI Overview  <externalLink><linkText>http://publib.boulder.ibm.com/infocenter/db2luw/v9/index.jsp?topic=/com.ibm.db2.udb.apdv.cli.doc/doc/c0023378.htm</linkText><linkUri>http://publib.boulder.ibm.com/infocenter/db2luw/v9/index.jsp?topic=/com.ibm.db2.udb.apdv.cli.doc/doc/c0023378.htm</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> An Overview of DB2 and Java DataBase Connectivity (JDBC)"  <externalLink><linkText>http://www.ibm.com/developerworks/data/library/techarticle/0203zikopoulos/0203zikopoulos.html</linkText><linkUri>http://www.ibm.com/developerworks/data/library/techarticle/0203zikopoulos/0203zikopoulos.html</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> Connecting to DB2 from SSIS  <externalLink><linkText>http://geekswithblogs.net/13DaysaWeek/archive/2010/05/16/connecting-to-db2-from-ssis.aspx</linkText><linkUri>http://geekswithblogs.net/13DaysaWeek/archive/2010/05/16/connecting-to-db2-from-ssis.aspx</linkUri></externalLink></para>
      <para>
        <superscript>10</superscript> SSIS and Data Sources  <externalLink><linkText>http://social.technet.microsoft.com/wiki/contents/articles/ssis-and-data-sources.aspx</linkText><linkUri>http://social.technet.microsoft.com/wiki/contents/articles/ssis-and-data-sources.aspx</linkUri></externalLink></para>
      <para>
        <superscript>11</superscript> SFTP Task  <externalLink><linkText>http://www.cozyroc.com/ssis/sftp-task</linkText><linkUri>http://www.cozyroc.com/ssis/sftp-task</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>