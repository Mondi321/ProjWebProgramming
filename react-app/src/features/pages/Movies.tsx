import { useEffect, useState } from "react";
// import Pagination2 from "../../components/Pagination/Pagination";
import LocalSearch from "../components/search/LocalSearch";
import SingleData from "../components/singleData/SingleData";
import Myloader from "react-spinners/PuffLoader";
import { useStore } from "../../app/stores/store";
import "./pagesStyle.css"
import { observer } from "mobx-react-lite";

const Movies = () => {
    const{movieStore}=useStore();
    const{movieRegistry, loadMovies, movies, loadingInitial} = movieStore;
    const[movieState, setMovieState] = useState(movies);
    useEffect(() => {
      window.scroll(0,0);
        if (movieRegistry.size <= 1) loadMovies();
        setMovieState(movies);
      }, [movieRegistry.size, loadMovies, movies])
  const [search, setSearch] = useState("");
  // eslint-disable-next-line
  const [color, setColor] = useState("grey");


function getMoviesSearch(){
  const newMovies = movieState.filter(movie => {
    if(movie.title.toLocaleLowerCase().includes(search.toLocaleLowerCase())){
      return movie;
    }
    return null;
  })
  setMovieState(newMovies);
}


  return (
    <>
      <main className="all__movies">
        <div className="my__main">
          <div className="TreadingHome">
            <h3> Movies:</h3>
          </div>
          <LocalSearch
            setSearch={setSearch}
            getMoviesSearch={getMoviesSearch}
            media="movies"
          />
        </div>

        <div className="ListContent">
          {!loadingInitial ? (
            movieState.map((movie) => (
              <SingleData key={movie.movieId} movieId={movie.movieId} rating={movie.rating} releaseYear={movie.releaseYear} title={movie.title} image={movie.image} />
            ))
          ) : (
            <div
              className="loading  "
              style={{
                display: "flex",
                height: "450px",

                justifyContent: "center",
                alignItems: "center",
              }}
            >
              <Myloader color={color} size={60} />
              <p
                style={{
                  color: "grey",
                  fontSize: "13px",
                  marginLeft: "10px",
                  marginTop: "10px",
                }}
              >
                fetching data ...
              </p>
            </div>
          )}
        </div>
      </main>
    </>
  );
};

export default observer(Movies);