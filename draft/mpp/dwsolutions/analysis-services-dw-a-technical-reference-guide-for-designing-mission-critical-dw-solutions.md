---
title: "Analysis Services (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 868f5c92-1909-45e2-92f7-ffc05a3713d6
caps.latest.revision: 5
manager: jeffreyg
---
# Analysis Services (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
  <summary />
  <introduction>
    <table xmlns:caps="http://schemas.microsoft.com/build/caps/2013/11">
      <tbody>
        <tr>
          <TD>
            <para>
              <embeddedLabel>Want more guides like this one?</embeddedLabel> Go to <externalLink><linkText>Technical Reference Guides for Designing Mission-Critical Solutions</linkText><linkUri>http://technet.microsoft.com/sqlserver/hh273157</linkUri></externalLink>.</para>
          </TD>
        </tr>
      </tbody>
    </table>
    <para>Microsoft SQL Server often serves as the source of both transactional (OLTP) data as well as pre-built data warehouse data. Key distinguishing features primarily focus on the connectivity to SQL Server as well as the geographical proximity of the SQL Server source to the Data Warehouse target. Other important items include volume, frequency of extract/load, complexity of data transformation, access windows to extract data.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide reference material and additional information.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>The Microsoft Data Warehouse Toolkit</linkText>
              <linkUri>http://www.rkimball.com/html/books.html</linkUri>
            </externalLink>
            <superscript>1</superscript>: This is an excellent book encompassing all aspects of implementing data warehouses and data marts using the entire Microsoft Suite of tools. Refer to Chapters 7, 8, and 10 for basic and advances implementation of SSAS. </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQLCAT's Guide to BI and Analytics</linkText>
              <linkUri>http://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf</linkUri>
            </externalLink>
            <superscript>2</superscript>: Many customers have deployed Analysis Services cubes in a "helter-skelter" manner and should follow a best practice for bringing the cubes into a more homogeneous/managed environment. This article provides guidelines for a holistic consolidation including for SQL Server/Hardware/SSAS. </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQLCAT's Guide to BI and Analytics</linkText>
              <linkUri>http://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf</linkUri>
            </externalLink>
            <superscript>3</superscript>: This article addresses specific memory management issues in conjunction with partitioned cubes for SSAS.<externalLink><linkText /><linkUri /></externalLink></para>
        </listItem>
      </list>
    </content>
    <sections>
      <section>
        <title>Analysis Services Distinct Count Optimization</title>
        <content>
          <list class="bullet">
            <listItem>
              <para>
                <externalLink>
                  <linkText>SQLCAT's Guide to BI and Analytics</linkText>
                  <linkUri>http://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf</linkUri>
                </externalLink>
                <superscript>4</superscript>: Distinct count (such as unique visitor counts on a web site) calculations provide valuable information but come with a number of performance challenges. This white paper describes tests that were performed to determine how best to optimize these calculations and includes best practices based on the test results. </para>
            </listItem>
            <listItem>
              <para>
                <externalLink>
                  <linkText>SQLCAT's Guide to BI and Analytics</linkText>
                  <linkUri>http://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf</linkUri>
                </externalLink>
                <superscript>5</superscript>: To expand on the distinct count optimization techniques provided in the Analysis Services Distinct Count Optimization Using Solid State Devices white paper, this technical note shows how using solid state devices (SSDs) can improve distinct count measures. </para>
            </listItem>
          </list>
        </content>
      </section>
    </sections>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>The following provide helpful information:</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Server Architecture (Analysis Services)</linkText>
              <linkUri>http://msdn.microsoft.com/library/ms174776.aspx</linkUri>
            </externalLink>
            <superscript>6</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Local Cubes (Analysis Services – Multidimensional Data)</linkText>
              <linkUri>http://msdn.microsoft.com/library/bb522640.aspx</linkUri>
            </externalLink>
            <superscript>7</superscript>
          </para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>Following are some question and consideration you can use when working with customers.</para>
      <list class="bullet">
        <listItem>
          <para>Analysis Services delivers much faster query performance than a relational system, but the cube(s) must be loaded and calculated.</para>
        </listItem>
        <listItem>
          <para>Carefully consider data volumes. </para>
        </listItem>
        <listItem>
          <para>Are there any Data Mining requirements? This is a powerful feature and often not utilized.</para>
        </listItem>
        <listItem>
          <para>SSAS provides a more convenient platform to deliver semi-additive facts than stand SQL queries.</para>
        </listItem>
        <listItem>
          <para>SSAS is not a substitute for a solid DW design/implementation. It will only augment the DW, not replace it.</para>
        </listItem>
        <listItem>
          <para>Clearly define and communicate what SSAS delivers, and what it doesn’t deliver.</para>
        </listItem>
        <listItem>
          <para>Determine if MOLAP, HOLAP, or ROLAP apply.</para>
        </listItem>
        <listItem>
          <para>Seven easy steps:</para>
          <list class="bullet">
            <listItem>
              <para>Set up the design/development environment</para>
            </listItem>
            <listItem>
              <para>Create a Data Source view (of the underlying DW)</para>
            </listItem>
            <listItem>
              <para>Create and fine-tune your dimensions</para>
            </listItem>
            <listItem>
              <para>Hierarchies</para>
            </listItem>
            <listItem>
              <para>Derived Attributes</para>
            </listItem>
            <listItem>
              <para>Many-to-many dimensions</para>
            </listItem>
            <listItem>
              <para>Run the Cube Wizard and edit the resulting cube (much faster than starting from scratch)</para>
            </listItem>
            <listItem>
              <para>Deploy the database to your development server</para>
            </listItem>
            <listItem>
              <para>Create calculations and other "decorations"</para>
            </listItem>
            <listItem>
              <para>Iterate, iterate, iterate</para>
            </listItem>
          </list>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text:</para>
      <para>
        <superscript>1 </superscript>The Microsoft Data Warehouse Toolkit  <externalLink><linkText>http://www.rkimball.com/html/books.html</linkText><linkUri>http://www.rkimball.com/html/books.html</linkUri></externalLink></para>
      <para>
        <superscript>2 </superscript>SQLCAT's Guide to BI and Analytics <externalLink><linkText>http://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf</linkText><linkUri>http://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> SQLCAT's Guide to BI and Analytics <externalLink><linkText>http://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf</linkText><linkUri>http://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Analysis Services Distinct Count Optimization <externalLink><linkText>http://www.microsoft.com/downloads/details.aspx?FamilyID=65df6ebf-9d1c-405f-84b1-08f492af52dd&amp;displaylang=en</linkText><linkUri>http://www.microsoft.com/downloads/details.aspx?FamilyID=65df6ebf-9d1c-405f-84b1-08f492af52dd&amp;displaylang=en</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> SQLCAT's Guide to BI and Analytics <externalLink><linkText>http://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf</linkText><linkUri>http://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Server Architecture (Analysis Services) <externalLink><linkText>http://msdn.microsoft.com/library/ms174776.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ms174776.aspx</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> Local Cubes (Analysis Services – Multidimensional Data) <externalLink><linkText>http://msdn.microsoft.com/library/bb522640.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb522640.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>