---
title: Search Text with Regular Expressions
description: Learn how to use a regular expression in the Find what field of a Find and Replace dialog box to specify a pattern to be matched.
author: markingmyname
ms.author: maghan
ms.date: 11/22/2022
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "vs.regularexpressionbuilder"
helpviewer_keywords:
  - "regular expressions [SQL Server Management Studio]"
  - "Query Editor [SQL Server Management Studio], regular expression searches"
  - "searches [SQL Server Management Studio], regular expressions"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Search Text with Regular Expressions

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Regular expressions are a concise and flexible notation for finding and replacing text patterns. A specific set of regular expressions can be used in the **Find what** field of the SQL Server Management Studio **Find and Replace** dialog box.

## Find using regular expressions

1. To enable the use of regular expressions in the **Find what** field during **QuickFind**, **FindinFiles**, **Quick Replace**, or **Replace in Files** operations, select the **Use** option under **Find Options** and choose **Regular expressions**.

1. The triangular **Reference List** button next to the **Find what** field then becomes available. Select this button to display a list of the most commonly used regular expressions. When you choose any item from the Expression Builder, it's inserted into the **Find what** string.

> [!NOTE]  
> There are syntax differences between the regular expressions that can be used in **Find what** strings and those that are valid in [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework programming. For example, in **Find and Replace**, the braces notation {} is used for tagged expressions. So the expression "zo{1}" matches all occurrences of "zo" followed by tag 1, as in "Alonzo1" and "Gonzo1". But within the .NET Framework, the notation {} is used for quantifiers. So the expression "zo{1}" matches all occurrences of "z" followed by exactly one "o," as in "zone" but not "zoo."

The following table describes the regular expressions available in the **Reference List**.

| Expression | Syntax | Description |
| --- | --- | --- |
| Any character | . | Matches any single character except a line break. |
| Zero or more | * | Matches zero or more occurrences of the preceding expression, making all possible matches. |
| One or more | + | Matches at least one occurrence of the preceding expression. |
| Beginning of line | ^ | Anchors the match string to the beginning of a line. |
| End of line | $ | Anchors the match string to the end of a line. |
| Beginning of word | \< | Matches only when a word begins at this point in the text. |
| End of word | > | Matches only when a word ends at this point in the text. |
| Line break | \n | Matches a platform-independent line break. In a Replace expression, inserts a line break. |
| Any one character in the set | [] | Matches any one of the characters within the []. To specify a range of characters, list the starting and ending characters separated by a dash (-), as in [a-z]. |
| Any one character not in the set | [^...] | Matches any character not in the set of characters following the ^. |
| Or | &#124; | Matches either the expression before or the one after the OR symbol (&#124;). Mostly used within a group. For example, (sponge&#124;mud) bath matches "sponge bath" and "mud bath." |
| Escape | \\ | Matches the character that follows the backslash (\\) as a literal. This feature allows you to find the characters used in regular expression notation, such as { and ^. For example, \\^ Searches for the ^ character. |
| Tagged expression | {} | Matches text tagged with the enclosed expression. |
| C/C++ Identifier | :i | Matches the expression ([a-zA-Z_$][a-zA-Z0-9_$]*). |
| Quoted string | :q | Matches the expression (("[^"]*")&#124;('[^']\*')). |
| Space or Tab | :b | Matches either space or tab characters. |
| Integer | :z | Matches the expression ([0-9]+). |

The list of all regular expressions that are valid in **Find and Replace** operations is longer than can be displayed in the **Reference List**. You can also insert any of the following regular expressions into a **Find what** string:

| Expression | Syntax | Description |
| --- | --- | --- |
| Minimal - zero or more | @ | Matches zero or more occurrences of the preceding expression, matching as few characters as possible. |
| Minimal - one or more | # | Matches one or more occurrences of the preceding expression, matching as few characters as possible. |
| Repeat n times | ^n | Matches n occurrences of the preceding expression. For example, [0-9]^4 matches any four-digit sequence. |
| Grouping | () | Groups a subexpression. |
| nth tagged text | \n | In a **Find or Replace** expression, indicates the text matched by the nth tagged expression, where n is a number from 1 to 9.<br /><br />In a **Replace** expression, \0 inserts the entire matched text. |
| Right-justified field | \\(w,n) | In a **Replace** expression, right-justifies the nth tagged expression in a field at least *w* characters wide. |
| Left-justified field | \\(-w,n) | In a **Replace** expression, left-justifies the nth tagged expression in a field at least *w* characters wide. |
| Prevent match | ~(X) | Prevents a match when X appears at this point in the expression. For example, real~(ity) matches the "real" in "realty" and "really," but not the "real" in "reality." |
| Alphanumeric character | :a | Matches the expression ([a-zA-Z0-9]). |
| Alphabetic character | :c | Matches the expression ([a-zA-Z]). |
| Decimal digit | :d | Matches the expression ([0-9]). |
| Hexadecimal digit | :h | Matches the expression ([0-9a-fA-F]+). |
| Rational number | :n | Matches the expression (([0-9]+.[0-9]*)&#124;([0-9]\*.[0-9]+)&#124;([0-9]+)). |
| Alphabetic string | :w | Matches the expression ([a-zA-Z]+). |
| Escape | \e | Unicode U+001B. |
| Bell | \g | Unicode U+0007. |
| Backspace | \h | Unicode U+0008. |
| Tab | \t | Matches a tab character, Unicode U+0009. |
| Unicode character | \x#### or \u#### | Matches a character given by Unicode value where #### is hexadecimal digits. You can specify a character outside the Basic Multilingual Plane (that is, a surrogate) with the ISO 10646 code point or with two Unicode code points giving the values of the surrogate pair. |

The following table lists the syntax for matching by standard Unicode character properties. The two-letter abbreviation is the same as listed in the Unicode character properties database. These expressions may be specified as part of a character set. For example, the expression [:Nd:Nl:No] matches any digit.

| Expression | Syntax | Description |
| --- | --- | --- |
| Uppercase letter | :Lu | Matches any one uppercase letter. For example, :Luhe matches "The" but not "the". |
| Lowercase letter | :Ll | Matches any one lowercase letter. For example, :Llhe matches "the" but not "The." |
| Title case letter | :Lt | Matches characters that combine an uppercase letter with a lowercase letter, such as Nj and Dz. |
| Modifier letter | :Lm | Matches letters or punctuation, such as commas, cross accents, and double prime, used to indicate modifications to the preceding letter. |
| Other letter | :Lo | Matches other letters, such as the gothic letter ahsa. |
| Decimal digit | :Nd | Matches decimal digits such as 0-9 and their full-width equivalents. |
| Letter digit | :Nl | Matches letter digits such as roman numerals and ideographic number zero. |
| Other digit | :No | Matches other digits such as old italic number one. |
| Open punctuation | :Ps | Matches opening punctuation such as open brackets and braces. |
| Close punctuation | :Pe | Matches closing punctuation such as closing brackets and braces. |
| Initial quote punctuation | :Pi | Matches initial double quotation marks. |
| Final quote punctuation | :Pf | Matches single quotation marks and ending double quotation marks. |
| Dash punctuation | :Pd | Matches the dash mark. |
| Connector punctuation | :Pc | Matches the underscore or underline mark. |
| Other punctuation | :Po | Matches (,), ?, ", !, @, #, %, &, *, \\, (:), (;), ', and /. |
| Space separator | :Zs | Matches blanks. |
| Line separator | :Zl | Matches the Unicode character U+2028. |
| Paragraph separator | :Zp | Matches the Unicode character U+2029. |
| Non-spacing mark | :Mn | Matches non-spacing marks. |
| Combining mark | :Mc | Matches combining marks. |
| Enclosing mark | :Me | Matches enclosing marks. |
| Math symbol | :Sm | Matches +, =, ~, &#124;, \<, and >. |
| Currency symbol | :Sc | Matches $ and other currency symbols. |
| Modifier symbol | :Sk | Matches modifier symbols such as circumflex accent, grave accent, and macron. |
| Other symbol | :So | Matches other symbols, such as the copyright sign, the pilcrow sign, and the degree sign. |
| Other control | :Cc | Matches end of line. |
| Other format | :Cf | Formatting control characters like the bi-directional control characters. |
| Surrogate | :Cs | Matches one half of a surrogate pair. |
| Other private-use | :Co | Matches any character from the private-use area. |
| Other not assigned | :Cn | Matches characters that don't map to a Unicode character. |
| Specify the number of occurrences of the preceding character or group. For more information, see [Match exactly n times](/dotnet/standard/base-types/quantifiers-in-regular-expressions#match-exactly-n-times-n). | {n}, where 'n' is the number of occurrences | `x(ab){2}x` matches "xababx"<br />`x(ab){2,3}x` matches "xababx" and "xabababx" but not "xababababx" |

In addition to the standard Unicode character properties, the following other properties may be specified as part of a character set.

| Expression | Syntax | Description |
| --- | --- | --- |
| Alpha | :Al | Matches any one character. For example, :Alhe matches words such as "The", "then", and "reached". |
| Numeric | :Nu | Matches any one number or digit. |
| Punctuation | :Pu | Matches any one punctuation mark, such as ?, @, ', and so on. |
| White space | :Wh | Matches all types of white space, including publishing and ideographic spaces. |
| Bidi | :Bi | Matches characters from right-to-left scripts such as Arabic and Hebrew. |
| Hangul | :Ha | Matches Korean Hangul and combines Jamos. |
| Hiragana | :Hi | Matches hiragana characters. |
| Katakana | :Ka | Matches katakana characters. |
| Ideographic/Han/Kanji | :Id | Matches ideographic characters, such as Han and Kanji. |

## Next steps

- [Search and Replace](./search-and-replace.md)
- [Search Text with Wildcards](./search-text-with-wildcards.md)
