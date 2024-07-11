---
title: "Brand the web portal"
description: Learn to brand your SQL Server Reporting Services (SSRS) or Power BI Report Server web portal's appearance to your business through a brand package.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/11/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: how-to
ms.custom: updatefrequency5
#customer intent: As a SQL Server system administrator, I want to customize the appearance of my SQL Server Reporting Services or Power BI Report Server web portal to align with my organization's branding. 
---

# Brand the web portal

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirs](../includes/ssrs-appliesto-pbirs.md)]

Learn how to customize the appearance of your SQL Server Reporting Services (SSRS) or Power BI Report Server web portal by creating and applying a brand package. A brand package allows you to change the web portal's appearance to match your business's branding. It includes colors, logos, and other styling elements packaged into a zip file. Then you upload the brand package to the web portal to apply the custom branding.

> [!VIDEO https://www.youtube-nocookie.com/embed/m08kLuofwFA]

## Prerequisites

- SQL Server Reporting Services (SSRS) or Power BI Report Server installed and configured.
- Access to the Reporting Services Web Portal.
- Connection to a report server database.

## Create the brand package

A brand package consists of three items that you package as a zip file. The following sections describe the items in the brand package and provide examples of the contents. 

You can create your own files from scratch or you can download samples from the GitHub site [Sample Branding Packages](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/reporting-services/branding) to a local folder. 

If you start from scratch, start by creating each file and naming the files in your brand package as follows:

- `metadata.xml`
- `colors.json`
- `logo.png` (optional file)

The zip file can be named anything you like.

If you choose to use a sample branding package, download the zip file and extract the files so that you can edit them to according to your needs.

### Define brand metadata (`metadata.xml`)

The `metadata.xml` file specifies the name of the brand package, and references the `colors.json` and `logo.png` files.

To change the name of your brand package, change the **name** attribute of the **SystemResourcePackage** element.

```xml
<?xml version="1.0" encoding="utf-8"?>
<SystemResourcePackage xmlns="http://schemas.microsoft.com/sqlserver/reporting/2016/01/systemresourcepackagemetadata"
    type="UniversalBrand"
    version="2.0.2"
    name="Multicolored example brand"
    >
</SystemResourcePackage>
```

You can include a logo in your brand package. This item is contained in the **Contents** element.

The following example doesn't include a logo file:

```xml
<?xml version="1.0" encoding="utf-8"?>
<SystemResourcePackage xmlns="http://schemas.microsoft.com/sqlserver/reporting/2016/01/systemresourcepackagemetadata"
    type="UniversalBrand"
    version="2.0.2"
    name="Multicolored example brand"
    >
    <Contents>
        <Item key="colors" path="colors.json" />
    </Contents>
</SystemResourcePackage>

```

The following example includes a logo file:

```xml
<?xml version="1.0" encoding="utf-8"?>
<SystemResourcePackage xmlns="http://schemas.microsoft.com/sqlserver/reporting/2016/01/systemresourcepackagemetadata"
    type="UniversalBrand"
    version="2.0.2"
    name="Multicolored example brand"
    >
    <Contents>
        <Item key="colors" path="colors.json" />
        <Item key="logo" path="logo.png" />
    </Contents>
</SystemResourcePackage>
```

### Define the color scheme (`colors.json`)

The `colors.json` file defines the color scheme for your brand package. When you upload the brand package, the server extracts the name/value pairs from this file and merges them with the primary LESS stylesheet, `brand.less`. It processes the stylesheet, and serves the resulting CSS file to the client. All colors in the stylesheet follow the six-character hexadecimal representation of a color.

Here’s an example of the `colors.json` file:

```json
{
    "name": "Multicolored example brand",
    "version": "1.0",
    "interface": {
        "primary": "#009900",
        "primaryContrast": "#ffffff",
        "secondary": "#042200",
        "neutralPrimary": "#d8edff",
        "neutralSecondary": "#e9d8eb",
        "danger": "#ff0000",
        "success": "#00ff00",
        "warning": "#ff8800"
    },
    "theme": {
        "dataPoints": ["#0072c6", "#f68c1f", "#269657"],
        "good": "#85ba00",
        "bad": "#e90000",
        "neutral": "#edb327"
    }
}
```
#### How LESS variables work

The LESS stylesheet contains blocks that reference predefined LESS variables. The following example shows how the stylesheet uses LESS variables:

```css
/* primary buttons */
.btn-primary {
    color:@primaryButtonColor;
    background-color:@primaryButtonBg;
}
```

While this syntax resembles CSS, the color values prefixed with the `@` symbol are unique to LESS. The `colors.json` file sets these variables.

For example, the `colors.json` file might include following values:

```json
"primary":"#009900",
"primaryContrast":"#ffffff"
```

When processed, the LESS variables map to the corresponding values in the `colors.json` file. The resulting CSS looks like the following example:

```css
.btn-primary {
    color: #ffffff;
    background-color: #009900;
} 
```

All of the primary buttons then render dark green with white text.

#### Objects in `colors.json`

The `colors.json` file includes two main objects:

- **Interface**: Properties specific to the web portal.
- **Theme**: Properties specific to the mobile reports that you create.

The `interface` object is broken down into the following properties:

|Section|Description|
|---|---|
|Primary|Button and hover colors.|
|Secondary|Title bar, search bar, left hand menu (if displayed), and text color for those items.|
|Neutral Primary|Home and report area backgrounds.|
|Neutral Secondary|Text box and folder options backgrounds, and the settings menu.|
|Neutral Tertiary|Site settings backgrounds.|
|Danger/Warning/Success messages|Colors for those messages.|
|KPI|Controls the colors for a good (1), neutral (0), neutral (-1), and none.|

The `theme` object is broken down into the following properties:

|Section|Description|
|---|---|
|Data Points|Colors for data points in charts and visualizations.|
|Good/Bad/Neutral|Colors indicating status.|
|Background|Overall background color.|
|Foreground|Overall foreground color.|
|Map Base|Base color for maps.|
|Panel Background/Foreground/Accent|Colors for panels.|
|Table Accents|Accent colors for tables.|

::: moniker range="<=sql-server-ver15"

The first time you connect to a server with a Mobile Report Publisher that has a brand package deployed, the publisher adds the theme to the list of available themes.

:::image type="content" source="../reporting-services/media/ssrsbrandingmobilereportpublisher.png" alt-text="Screenshot of the Choose a color palette dialog.":::

You can then use that theme for any mobile reports that you create, even if they aren't for the same server that you have the theme deployed on.
::: moniker-end

### Use a logo (`logo.png`)

If you include a logo with your brand package, it appears in the web portal in place of the name you originally set for the web portal.

Make sure the logo is in the PNG file format. The file dimensions scale once uploaded to the server. The logo scales to approximately 290 x 60 pixels.

## <a name="#applying-the-brand-package-to-the-web-portal"></a>Apply the brand package to the web portal

1. Access the web portal.

1. Select the gear icon in the upper right, and then choose **Site Settings**.

    :::image type="content" source="../reporting-services/media/ssrsgearmenu.png" alt-text="Screenshot of the Settings list with Site Settings option highlighted.":::

1. Select **Branding**.

    :::image type="content" source="../reporting-services/media/ssrsbranding.png" alt-text="Screenshot of the Site Settings page with the Branding tab highlighted.":::

   **Currently installed brand package** either displays the name of the uploaded package, or it displays **None**.

1. Select **Upload brand package**. The brand package uploads to the report server and the web portal renders the updated branding immediately.

## Download or remove the brand package

If you see a brand package listed in the **Currently installed brand package** box, you can choose to download or remove the package. You might want to download the package if you want to make adjustments to the existing package and apply those changes. If you remove the package, the web portal resets to the default brand immediately. Choose either **Download** or **Remove** depending on the action you want to take.

## `metadata.xml` example

```xml
<?xml version="1.0" encoding="utf-8"?>
<SystemResourcePackage xmlns="http://schemas.microsoft.com/sqlserver/reporting/2016/01/systemresourcepackagemetadata"
    type="UniversalBrand"
    version="2.0.2"
    name="Multicolored example brand"
    >
    <Contents>
        <Item key="colors" path="colors.json" />
        <Item key="logo" path="logo.png" />
    </Contents>
</SystemResourcePackage>
```

## `colors.json` example

```json
{
    "name":"Multicolored example brand",
    "version":"1.0",
    "interface":{
        "primary":"#b31e1e",
        "primaryAlt":"#ca0806",
        "primaryAlt2":"#621013",
        "primaryAlt3":"#e40000",
        "primaryAlt4":"#e14e50",
        "primaryContrast":"#fff",

        "secondary":"#042200",
        "secondaryAlt":"#0f4400",
        "secondaryAlt2":"#155500",
        "secondaryAlt3":"#217700",
        "secondaryContrast":"#49e63c",

        "neutralPrimary":"#d8edff",
        "neutralPrimaryAlt":"#c9e6ff",
        "neutralPrimaryAlt2":"#aedaff",
        "neutralPrimaryAlt3":"#88c8ff",
        "neutralPrimaryContrast":"#0a2b4c",

        "neutralSecondary":"#e9d8eb",
        "neutralSecondaryAlt":"#d9badc",
        "neutralSecondaryAlt2":"#b06cb5",
        "neutralSecondaryAlt3":"#a75bac",
        "neutralSecondaryContrast":"#250a26",

        "neutralTertiary":"#f79220",
        "neutralTertiaryAlt":"#f8a54b",
        "neutralTertiaryAlt2":"#facc9b",
        "neutralTertiaryAlt3":"#fce3c7",
        "neutralTertiaryContrast":"#391d00",

        "danger":"#ff0000",
        "success":"#00ff00",
        "warning":"#ff8800",
        "info":"#00ff",
        "dangerContrast":"#fff",
        "successContrast":"#fff",
        "warningContrast":"#fff",
        "infoContrast":"#fff",

        "kpiGood":"#4fb443",
        "kpiBad":"#de061a",
        "kpiNeutral":"#d9b42c",
        "kpiNone":"#333",
        "kpiGoodContrast":"#fff",
        "kpiBadContrast":"#fff",
        "kpiNeutralContrast":"#fff",
        "kpiNoneContrast":"#fff",
        
        "itemTypeIconColor":"#ffffff",
        "reportIconBackground":"#12239e",
        "excelIconBackground":"#217346",
        "folderIconBackground":"#4668c5",
        "datasetIconBackground":"#c94f0f",
        "otherIconBackground":"#000000", 
        
        "primaryButton": "#bb2124",
        "primaryButtonHover": "#d31115",
        "primaryButtonPressed": "#3d0000", 
        
        "link": "#d31115",
        "linkHover": "#671215",
        "linkVisited": "#3d0000", 
        
        "radioButtonCheckBox": "#bb2124",
        "radioButtonCheckBoxHover": "#d31115"
        },
        "theme":{
        "dataPoints":[
            "#0072c6",
            "#f68c1f",
            "#269657",
            "#dd5900",
            "#5b3573",
            "#22bdef",
            "#b4009e",
            "#008274",
            "#fdc336",
            "#ea3c00",
            "#00188f",
            "#9f9f9f"
        ],  

        "good":"#85ba00",
        "bad":"#e90000",
        "neutral":"#edb327",
        "none":"#333",

        "background":"#fff",
        "foreground":"#222",
        "mapBase":"#00aeef",
        "panelBackground":"#f6f6f6",
        "panelForeground":"#222",
        "panelAccent":"#00aeef",
        "tableAccent":"#00aeef",

        "altBackground":"#f6f6f6",
        "altForeground":"#000",
        "altMapBase":"#f68c1f",
        "altPanelBackground":"#235378",
        "altPanelForeground":"#fff",
        "altPanelAccent":"#fdc336",
        "altTableAccent":"#fdc336"
    }
}
```

More questions? Try asking the [Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user).
