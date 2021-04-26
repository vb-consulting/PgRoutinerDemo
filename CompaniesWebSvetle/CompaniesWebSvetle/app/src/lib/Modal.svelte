<script lang="ts">
export let title: string = "";

let modalDisplay = "none";
let modalClass = "";
let showBackdrop = false;

export function open() {
    modalClass = "show";
    setTimeout(() => {
        modalDisplay = "block";
        showBackdrop = true;
    }, 100);
}

export function close() {
    modalClass = "";
    setTimeout(() => {
        modalDisplay = "none";
        showBackdrop = false;
    }, 250);
}
</script>

<div class="modal {modalClass}" tabindex="-1" style="display:{modalDisplay}; overflow-y: auto;">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">{title}</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" on:click={close}></button>
            </div>
            <div class="modal-body">
                <slot name="body"></slot>
            </div>
            <div class="modal-footer">
                <slot name="footer"></slot>
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" on:click={close}>Close</button>
            </div>
        </div>
    </div>
</div>
{#if showBackdrop}
<div class="modal-backdrop fade {modalClass}" style="display: {modalDisplay}"></div>
{/if}