import { useEffect, useState } from "react";
import SingleData from "../components/singleData/SingleData";
import "./homePage.css";
import { Link } from "react-router-dom";
import HomeNav from "../components/homeNav/HomeNav";
import Myloader from "react-spinners/PuffLoader";
import { useStore } from "../../app/stores/store";
import "./pagesStyle.css";
import { observer } from "mobx-react-lite";
import SingleDataTvShow from "../components/singleData/SingleDataTvShow";

const Home = () => {
  const { movieStore, tvShowStore } = useStore();
  const { movieRegistry, loadMovies, loadingInitial, movies7, movieRegistryTopRated, loadMoviesTopRated, moviesTopRated } = movieStore;
  const{tvShowRegistry, loadTvShows, loadingInitialTvShow, tvShows7} = tvShowStore;

  useEffect(() => {
    if (movieRegistry.size <= 1) loadMovies();
  }, [movieRegistry.size, loadMovies])

  useEffect(() => {
    if (tvShowRegistry.size <= 1) loadTvShows();
  }, [tvShowRegistry.size, loadTvShows])

  useEffect(() => {
    if (movieRegistryTopRated.size <= 1) loadMoviesTopRated();
  }, [movieRegistryTopRated.size, loadMoviesTopRated])

  useEffect(() => {
    window.scroll(0,0);
  }, [])
  

  // eslint-disable-next-line
  let [color, setColor] = useState("grey");


  return (
    <>
      {!loadingInitial || !loadingInitialTvShow ? (
        <>
          <div style={{ marginTop: "-10px" }} className="bg__home">
            <HomeNav />
          </div>
          <div className="TreadingHome3 pt-4">
            <div className="title__home">
              <div className="btn__home">
                <h6>
                  Movies On Air &#160;
                  <span style={{ paddingTop: "10px" }}>&#11166;</span>
                </h6>
              </div>
              <div className="view__more">
                <Link to="/all-movies" style={{ textDecoration: "none" }}>
                  <p>View more &#187;</p>
                </Link>
              </div>
            </div>
 
            <div className="ListContent2">
              {movies7 &&
                movies7.map((movie) => (
                  <SingleData key={movie.movieId} movieId={movie.movieId} rating={movie.rating} title={movie.title} releaseYear={movie.releaseYear} image={movie.image} />
                ))}

            </div>
          </div>
          <hr />
          <div className="TreadingHome3">
            <div className="title__home">
              <div className="btn__home">
                <h6>
                  TvSeries On Air &#160;
                  <span style={{ paddingTop: "10px" }}>&#11166;</span>
                </h6>
              </div>
              <div className="view__more">
                <Link to="/all-series" style={{ textDecoration: "none" }}>
                  <p>View more &#187;</p>
                </Link>
              </div>
            </div>
            <div className="ListContent2">
              {tvShows7 &&
                tvShows7.map((tvShow) => (
                  <SingleDataTvShow key={tvShow.tvShowId} title={tvShow.title} tvShowId={tvShow.tvShowId} rating={tvShow.rating} releaseYear={tvShow.releaseYear} image={tvShow.image} />
                ))}
            </div>
          </div>
          <hr />
          <div className="TreadingHome3">
            <div className="title__home">
              <div className="btn__home" style={{ width: "160px" }}>
                <h6>
                  Top Rated &#160;
                  <span style={{ paddingTop: "10px" }}>&#11166;</span>
                </h6>
              </div>
              <div className="view__more">
                <Link to="/all-movies" style={{ textDecoration: "none" }}>
                  <p>View more &#187;</p>
                </Link>
              </div>
            </div>
            <div className="ListContent2">
              {moviesTopRated &&
                moviesTopRated.map((movie) => (
                  <SingleData key={movie.movieId} movieId={movie.movieId} title={movie.title} rating={movie.rating} releaseYear={movie.releaseYear} image={movie.image}/>
                ))}
            </div>
          </div>
        </>
      ) : (
        <div className="major" style={{ height: "600px" }}>
          <Myloader color={color} size={60} />
        </div>
      )}
    </>
  );
};

export default observer(Home);