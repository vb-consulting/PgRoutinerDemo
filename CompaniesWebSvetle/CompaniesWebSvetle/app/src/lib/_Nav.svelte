<script lang="ts">
export let brandLink: string = "";
export let brandContent: string;
</script>

<nav class="navbar fixed-top navbar-expand-lg navbar-dark bg-primary">
    <div class="container">
        <a class="navbar-brand me-1 mb-1 mt-0" href="{brandLink}">
            {brandContent}
        </a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsingNavbar">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="navbar-collapse collapse justify-content-center" id="collapsingNavbar">
            <ul class="navbar-nav ml-auto">
                <slot></slot>
            </ul>
        </div>
    </div>
</nav>