document.addEventListener("DOMContentLoaded", function () {
    const commentForm = document.getElementById("comment-form");
    const commentsList = document.querySelector(".comments-list");

    commentForm.addEventListener("submit", function (event) {
      event.preventDefault();

      
      const commentText = document.getElementById("comment-text").value;

      
      const newComment = document.createElement("li");
      newComment.className = "comment";
      newComment.innerHTML = `Name:<span>${commentText}</span>`;

     
      commentsList.appendChild(newComment);

      
      document.getElementById("comment-text").value = "";
    });
  });
