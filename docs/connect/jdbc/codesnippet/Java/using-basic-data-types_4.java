PreparedStatement pstmt = con.prepareStatement("UPDATE employee SET
   fname = ? WHERE (lname = 'Brown')");
String first = "Bob";
pstmt.setString(1, first);
int rowCount = pstmt.executeUpdate();
pstmt.close();