// wwwroot/js/site.js
window.scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
};

window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    const scrollToTopButton = document.querySelector('.scroll-to-top');
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        scrollToTopButton.style.display = "block";
    } else {
        scrollToTopButton.style.display = "none";
    }
}



