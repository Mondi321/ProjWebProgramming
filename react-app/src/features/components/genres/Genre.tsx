import axios from "axios";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { Link } from "react-router-dom";
import { Genre } from "../../../app/models/genre";
import { Movie } from "../../../app/models/movie";
import { useStore } from "../../../app/stores/store";
import "./genre.css";

interface Props{
    setMovieState: (movies:Movie[]) => void;
    media: string;
    movieState: Movie[];
}

const Genres = ({setMovieState, media, movieState}:Props) => {

  const{genreStore, movieStore} = useStore();
  const{genreRegistry, loadGenres, genres} = genreStore;

  useEffect(() => {
    if (genreRegistry.size <= 1) loadGenres();
  }, [genreRegistry.size, loadGenres])


    const handleFilter = (genre?:Genre) => {
        if(!genre){
            window.location.reload();
        }
        if(media === "movie"){
            // let movieByGenre:Movie[] =new Array;
            movieState.forEach(movie => {
              console.log(movie);
            })
            const movieByGenre = movieState.filter(movie => {
              if(movie.genres?.includes(genre!)){
                return movie; 
              }
            })
            console.log(movieByGenre)
            setMovieState(movieByGenre);
        }
    }

//   useEffect(() => {
//     fetchGenres();
//     return () => {
//       setGetGenre();
//     };
//     // eslint-disable-next-line
//   }, []);

  return (
    <>
      <div className="dropdown" style={{ position: "relative" }}>
        <Link
          className="btn btn-secondary dropdown-toggle mybtn"
          to="#"
          role='button'
          id="dropdownMenuLink"
          data-bs-toggle='dropdown'
          aria-expanded="false"
        >
          Filter By:{" "}
        </Link>

        <div className="dropdown-menu" aria-labelledby="dropdownMenuLink">
          <div className="title__genre">Categories</div>

          <div className="category__content">
            <p className="dropdown-item3" onClick={() => handleFilter()}>
              all {media === "movie" ? "Movies" : "Tv series"}
            </p>
            {genres &&
              genres.map((genre) => (
                <p
                  key={genre.genreId}
                  onClick={() => handleFilter(genre)}
                  className="dropdown-item2"
                >
                  {genre.name}
                </p>
              ))}
          </div>
        </div>
      </div>
    </>
  );
};

export default observer(Genres);