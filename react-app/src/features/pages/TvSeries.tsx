import { useEffect, useState } from "react";
import LocalSearch from "../components/search/LocalSearch";
import Myloader from "react-spinners/PuffLoader";
import { useStore } from "../../app/stores/store";
import SingleDataTvShow from "../components/singleData/SingleDataTvShow";
import { observer } from "mobx-react-lite";
import './pagesStyle.css';

const TvSeries = () => {
    const{tvShowStore}=useStore();
    const{tvShowRegistry, loadTvShows, tvShows, loadingInitialTvShow} = tvShowStore;
    const[tvShowState, setTvShowState] = useState(tvShows);
    useEffect(() => {
      window.scroll(0,0);
        if (tvShowRegistry.size <= 1) loadTvShows();
        setTvShowState(tvShows);
      }, [tvShowRegistry.size, loadTvShows, tvShows])
  const [search, setSearch] = useState("");
  // eslint-disable-next-line
  const [color, setColor] = useState("grey");

  function getTvShowsSearch(){
    const newTvShows = tvShowState.filter(tvShow => {
      if(tvShow.title.toLocaleLowerCase().includes(search.toLocaleLowerCase())){
        return tvShow;
      }
      return null;
    })
    setTvShowState(newTvShows);
  }

  return (
    <>
      <main className="all__series">
        <div className="my__main">
          <div className="TreadingHome">
            <h3>TV series:</h3>
          </div>
          <LocalSearch
            setSearch={setSearch}
            getTvShowsSearch={getTvShowsSearch}
            media="tv-series"
          />
        </div>

        <div className="ListContent">
          {!loadingInitialTvShow ? (
            tvShowState.map((tvShow) => (
              <SingleDataTvShow key={tvShow.tvShowId} title={tvShow.title} rating={tvShow.rating} releaseYear={tvShow.releaseYear} tvShowId={tvShow.tvShowId} image ={tvShow.image} />
            ))
          ) : (
            <div
              className="loading"
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

export default observer(TvSeries);