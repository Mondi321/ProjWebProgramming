import * as React from "react";
import Box from "@mui/material/Box";
import MuiBottomNavigation from "@mui/material/BottomNavigation";
import MuiBottomNavigationAction from "@mui/material/BottomNavigationAction";
import { useEffect } from "react";
import MovieIcon from "@mui/icons-material/Movie";
import TheatersIcon from "@mui/icons-material/Theaters";
import HomeIcon from "@mui/icons-material/Home";
import ConfirmationNumberIcon from '@mui/icons-material/ConfirmationNumber';
import { useHistory } from "react-router-dom";
import { styled } from "@mui/material/styles";

import "./bottomNav.css";
const BottomNavigationAction = styled(MuiBottomNavigationAction)(`

  &.MuiBottomNavigationAction-root{
    color:#abb7c4;
  },  &.Mui-selected {
    color: #1a9be7;
  }
  
`);
const BottomNavigation = styled(MuiBottomNavigation)(`
    background:rgba(1, 0, 12, 0.5) !important;
    border-top-right-radius: 10px !important;
    border-top-left-radius: 10px !important;


 
`);
export default function BottomNav() {
  const [value, setValue] = React.useState("");
  const history = useHistory();
  useEffect(() => {
    if (value === "home") history.push("/home");
    else if (value === "movies") history.push("/all-movies");
    else if (value === "series") history.push("/all-series");
    else if (value === "eticket") window.location.href = 'https://localhost:7115/Identity/Account/Login?ReturnUrl=%2FMovies';
    // eslint-disable-next-line
  }, [value, history]);

  return (
    <Box
      style={{ zIndex: "2000" }}
      className="BottomNav"
      sx={{
        width: "100%",
        position: "fixed",
        bottom: 0,
        justifyContent: "center",
      }}
    >
      <BottomNavigation
        style={{
          background: "black",
        }}
        showLabels
        value={value}
        onChange={(event, newValue) => {
          setValue(newValue);
        }}
      >
        <BottomNavigationAction
          className="BottomNavIcon"
          label="Home"
          icon={<HomeIcon />}
          color="primary"
          value="home"
        />
        <BottomNavigationAction
          label="Movies"
          icon={<MovieIcon />}
          value="movies"
        />
        <BottomNavigationAction
          label="Series"
          icon={<TheatersIcon />}
          value="series"
        />
                <BottomNavigationAction
          label="eTicket"
          icon={<ConfirmationNumberIcon />}
          value="eticket"
        />
      </BottomNavigation>
    </Box>
  );
}