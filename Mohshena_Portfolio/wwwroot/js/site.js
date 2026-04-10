// Theme toggle
const toggle = document.getElementById("theme-toggle");
toggle.addEventListener("click", () => {
    document.body.classList.toggle("light-mode");
});

// Mobile menu toggle
function toggleMenu() {
    const navLinks = document.getElementById("navLinks");
    navLinks.classList.toggle("active");
}

// Close menu when clicking outside of it
document.addEventListener("click", function (event) {
    const navLinks = document.getElementById("navLinks");
    const menuToggle = document.querySelector(".menu-toggle");

    // If menu is open and click is outside both the navLinks and the toggle button
    if (navLinks.classList.contains("active") &&
        !navLinks.contains(event.target) &&
        !menuToggle.contains(event.target)) {
        navLinks.classList.remove("active");
    }
});

// Close menu when a nav link is clicked (optional but good UX)
document.querySelectorAll(".nav-links a").forEach(link => {
    link.addEventListener("click", () => {
        document.getElementById("navLinks").classList.remove("active");
    });
});

// Typewriter effect
const role = ".Net Developer";
let charIndex = 0;
const dynamicText = document.querySelector('.dynamic-text');

function animateText() {
    dynamicText.textContent = role.substring(0, charIndex);
    charIndex++;
    if (charIndex <= role.length) {
        setTimeout(animateText, 150); // typing speed
    } else {
        // Wait then restart
        setTimeout(() => { charIndex = 0; animateText(); }, 2000);
    }
}
animateText();

// Fade-up animations for sections
const sections = document.querySelectorAll('.about-section, .skills-section, .projects-section');

const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('show');
            observer.unobserve(entry.target);
        }
    });
}, { threshold: 0.2 });

sections.forEach(section => observer.observe(section));