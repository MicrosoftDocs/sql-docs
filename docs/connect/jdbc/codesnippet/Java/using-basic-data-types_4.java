try(PreparedStatement pstmt = con.prepareStatement("UPDATE employee SET fname = ? WHERE (lname = 'Brown')");) {
    String name = "Bob";
    pstmt.setString(1, name);
    int rowCount = pstmt.executeUpdate();
}
