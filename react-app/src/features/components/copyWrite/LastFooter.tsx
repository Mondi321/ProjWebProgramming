import "./lastFooter.css";
import $ from "jquery";

const LastFooter = () => {
  function scrollToTop() {
    $(window).scrollTop(0);
}
  return (
    <>
      <div className="write__footer">
        <div className="container2 ">
          <span>
            © 2022 CinemyPlex. All Rights Reserved. Designed by{" "}
            <a href="#">Edmond Halili</a>.
          </span>
          <h6 className="scroll-to-top">
            <span onClick={scrollToTop} id="toTop">Back to top &#8593;</span>
            <span className=""></span>
          </h6>
        </div>
      </div>
    </>
  );
};

export default LastFooter;