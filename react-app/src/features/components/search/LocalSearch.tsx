import * as React from "react";
import "./localSearch.css";
import SearchIcon from "@mui/icons-material/Search";
import { createTheme, ThemeProvider } from "@material-ui/core";

interface Props{
    setSearch: (search:string) => void;
    getMoviesSearch?: () => void;
    getTvShowsSearch?: () => void;
    media: string;
}

export default function LocalSearch({setSearch, getMoviesSearch, getTvShowsSearch, media}:Props) {
  // eslint-disable-next-line
  const darkTheme = createTheme({
    palette: {
      type: "dark",
      primary: {
        main: "#abb7c4;",
      },
    },
  });

  const handleSearch = () => {
    if(media === "movies"){
      getMoviesSearch!();
    }
    else{
      getTvShowsSearch!();
    }
  };
  const handleChange = (e:any) => {
    e.preventDefault();
    setSearch(e.target.value);
  };

  return (
    <>
      <ThemeProvider theme={darkTheme}>
        <div className="search">
          <div className="form_search">
            <input
              type="search"
              placeholder="Search Movies Here ..."
              onChange={handleChange}
            />
            <SearchIcon className="icon" />
            <div
              className="btn btn-primary brn-sm search__icon"
              onClick={handleSearch}
            >
              Search
            </div>
          </div>
        </div>
      </ThemeProvider>
    </>
  );
}
