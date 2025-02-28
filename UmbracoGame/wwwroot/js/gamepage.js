   function openReviewsPopup() {
        document.getElementById("reviewsPopup").style.display = "flex";

        var gameId = "@Model.Game.Id"; // Get Game ID from Razor
        console.log("Fetching reviews for GameId:", gameId);

        fetch(`/umbraco/surface/ReviewSurface/GetReviews?gameId=${gameId}`)
            .then(response => response.json())
            .then(data => {
                console.log("Fetched data:", data);
                const reviewsContainer = document.getElementById("reviewsContainer");
                reviewsContainer.innerHTML = "";

                if (!data || data.length === 0) {
                    reviewsContainer.innerHTML = "<p>No reviews yet. Be the first to write one!</p>";
                    return;
                }

                data.forEach(review => {
                    const reviewElement = document.createElement("div");
                    reviewElement.classList.add("review-item", "mb-3", "p-3", "border", "rounded");

                    reviewElement.innerHTML = `
                        <strong>${review.name || "Unknown"}</strong> (${review.rating || "N/A"}/5)
                        <br />
                        <em>${new Date(review.date).toLocaleString()}</em>
                        <p>${review.comment || "No comment"}</p>
                    `;

                    reviewsContainer.appendChild(reviewElement);
                });
            })
            .catch(error => console.error("Error loading reviews:", error));
    }

    function closeReviewsPopup() {
        document.getElementById("reviewsPopup").style.display = "none";
    }

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("addGameForm").addEventListener("submit", function (event) {
        event.preventDefault(); // Prevent default form submission

        var formData = new FormData(this);

        fetch("@Url.Action("AddGame", "GameSurface")", {
            method: "POST",
            body: formData,
            headers: {
                "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                // Change button text and disable it
                let addButton = document.getElementById("add-game");
                addButton.innerText = "Added";
                addButton.disabled = true;

                // Show success popup
                let popup = document.getElementById("success-popup");
                popup.style.display = "block";
                popup.style.opacity = "1";

                // Hide popup after 4 seconds
                setTimeout(() => {
                    popup.style.opacity = "0";
                    setTimeout(() => popup.style.display = "none", 500);
                }, 4000);
            } else {
                alert(data.message); // Show error message
            }
        })
        .catch(error => console.error("Error:", error));
    });
});



    function openModal() {
        document.getElementById('mediaModal').style.display = 'block';
    }

    function closeModal() {
        document.getElementById('mediaModal').style.display = 'none';
    }

    function openImageModal(imageUrl) {
        document.getElementById('enlargedImage').src = imageUrl;
        document.getElementById('imageModal').style.display = 'block';
    }

    function closeImageModal() {
        document.getElementById('imageModal').style.display = 'none';
    }

        function openPopup() {
        document.getElementById("reviewPopup").style.display = "flex";
    }

    function closePopup() {
        document.getElementById("reviewPopup").style.display = "none";
    }
</script>