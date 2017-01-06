---
title: "Third-Party Consumers (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a7017c51-6d60-4af9-9f36-d69980e86222
caps.latest.revision: 2
manager: jeffreyg
---
# Third-Party Consumers (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>The world is full of products claiming to be in the Business Intelligence business. It is refreshing to note that the primary third-party products in this space include:</para>
    <list class="bullet">
      <listItem>
        <para>Business Objects</para>
      </listItem>
      <listItem>
        <para>Cognos</para>
      </listItem>
      <listItem>
        <para>MicroStrategy </para>
      </listItem>
    </list>
    <para>This is the order in terms of market-share. Business Objects was acquired by SAP in 2008 and Cognos by IBM in 2009. All three products are enterprise-class technologies and one or more of them exists in every major organization world-wide. All three products have been around since the mid-90’s and have matured accordingly. Their primary architecture requires of a powerful "BI server" where analytical &amp; reporting data are intelligently cached. COGNOS helped to pioneer Multi-dimensional databases and have a long-standing reputation for high-speed cube-based analytics. All three provide very powerful and integrated metadata for reporting, and in the case of Business Objects, even for the ETL components.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>Most third-party BI tools provide a metadata layer that insulates the BI Application from the underlying database. More recently the major contenders have developed elaborate metadata layers to simplify changes made to the underlying database. Be sure to understand how any changes to Microsoft SQL Server/PDW database design will be handled by the third-party tools.</para>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>Determine how embedded the third-party tools are to the customer’s BI infrastructure:</para>
          <list class="bullet">
            <listItem>
              <para>Would they be willing to invest in converting to the Microsoft BI Stack?</para>
            </listItem>
            <listItem>
              <para>If so, determine effort/time involved for conversion.</para>
            </listItem>
          </list>
        </listItem>
      </list>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>