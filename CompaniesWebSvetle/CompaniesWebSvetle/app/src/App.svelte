<script lang="ts">
    import Nav from "./lib/_Nav.svelte";
    import Footer from "./lib/_Footer.svelte";
    import Index from "./lib/Index.svelte";

    //import { get } from './fetch';
  //import Counter from './lib/Counter.svelte'
  /*
  async function getText() {
    const res = await get("api/test");
    const text = await res.text();
    if (res.ok) {
      return text;
    } else {
      throw new Error(text);
    }
  }

  let promise = getText();
  */
</script>

<Nav brandContent="PgRoutiner Demo" />
<Index />
<Footer />

<style lang="scss">
</style>
